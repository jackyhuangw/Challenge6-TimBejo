using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class InteractablePickup : MonoBehaviour
{
    [Header("Identity")]
    public string itemId = "item_001";     // unik per item
    public bool destroyOnCollect = true;   // atau setActive(false)

    [Header("UI Prompt")]
    public GameObject interactPrompt;      // ikon/teks di atas item (opsional)

    [Header("Behavior")]
    public bool collectOnContact = true;   // auto collect saat pemain masuk trigger

    [Header("Events (optional)")]
    public UnityEvent onCollected;

    [HideInInspector] public bool playerInRange = false;

    void Start()
    {
        // Jika sudah pernah diambil, sembunyikan langsung
        if (Inventory.Instance != null && Inventory.Instance.Has(itemId))
        {
            if (destroyOnCollect) Destroy(gameObject);
            else gameObject.SetActive(false);
        }
        if (interactPrompt) interactPrompt.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactPrompt) interactPrompt.SetActive(true);
            // beritahu player bahwa ia bisa berinteraksi dengan objek ini
            var pi = col.GetComponent<PlayerInteraction>();
            if (pi) pi.SetCurrent(this);

            // auto collect jika diaktifkan, namun hanya jika tidak ada UI button
            bool noUIButtonConfigured = (pi == null || pi.interactButton == null);
            if (collectOnContact && noUIButtonConfigured)
            {
                Interact();
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactPrompt) interactPrompt.SetActive(false);
            var pi = col.GetComponent<PlayerInteraction>();
            if (pi && pi.Current == this) pi.ClearCurrent(this);
        }
    }

    public void Interact()  // dipanggil saat tombol ditekan
    {
        if (!playerInRange) return;

        Inventory.Instance?.Add(itemId);
        onCollected?.Invoke();

        if (interactPrompt) interactPrompt.SetActive(false);

        if (destroyOnCollect) Destroy(gameObject);
        else gameObject.SetActive(false);
    }
}
