using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [Header("UI")]
    public Button interactButton;  // drag InteractButton
    public GameObject bottomUI;    // opsional: parent UI, kalau mau sembunyikan

    public InteractablePickup Current { get; private set; }

    void Start()
    {
        if (interactButton != null)
        {
            interactButton.onClick.AddListener(TryInteract);
            interactButton.gameObject.SetActive(false); // hidden di awal
        }
    }

    void Update()
    {
        // Keyboard fallback
        if (Current != null && Current.playerInRange && Input.GetKeyDown(KeyCode.E))
            TryInteract();

        // Safety: sync visibilitas tombol dengan kondisi range
        if (interactButton != null)
            interactButton.gameObject.SetActive(Current != null && Current.playerInRange);
    }

    public void SetCurrent(InteractablePickup pick)
    {
        Current = pick;
        if (interactButton != null)
            interactButton.gameObject.SetActive(true);
    }

    public void ClearCurrent(InteractablePickup pick)
    {
        if (Current == pick) Current = null;
        if (interactButton != null)
            interactButton.gameObject.SetActive(false);
    }

    // Dipanggil oleh tombol UI dan keyboard
    public void TryInteract()
    {
        if (Current != null && Current.playerInRange)
            Current.Interact();
    }
}
