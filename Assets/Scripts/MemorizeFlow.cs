using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Cinemachine;
using UnityEngine.Rendering.Universal; // Light2D

public class MemorizeFlow : MonoBehaviour
{
    [Header("Cinemachine")]
    public CinemachineCamera vcamOverview;
    public CinemachineCamera vcamPlayer;

    [Header("UI Containers")]
    public TMP_Text timerText;
    public GameObject memorizeTimeUI;  // drag MemorizeTime
    public GameObject bottomUI;        // drag Bottom_UI

    [Header("Lights (URP 2D)")]
    public Light2D globalLight;       // drag Global Light 2D
    public Light2D playerLight;       // drag child Light2D di Player

    [Header("Memorize Settings")]
    public float memorizeDuration = 5f;   // detik
    public float darkIntensity = 0.1f;    // target gelap
    public float fadeTime = 0.6f;         // durasi fade

    [Header("Player Control (opsional)")]
    public MonoBehaviour playerController;    // skrip gerak player
    public string canMoveFieldName = "canMove";

    float endTime;
    bool memorizing = true;

    void Start()
{
    // Kamera mulai overview
    SetPriority(overviewHigh: true);

    // UI awal
    if (bottomUI) bottomUI.SetActive(false);        // sembunyikan kontrol
    if (memorizeTimeUI) memorizeTimeUI.SetActive(true); // tampilkan timer

    // Player tidak bisa gerak
    SetPlayerCanMove(false);

    // Lampu: terang global, matikan lampu player
    if (globalLight) globalLight.intensity = 1f;
    if (playerLight) playerLight.enabled = false;

    // Mulai timer memorize
    memorizing = true;
    endTime = Time.time + memorizeDuration;
}

    void Update()
    {
        if (!memorizing) return;

        float remain = Mathf.Max(0, endTime - Time.time);
        if (timerText) timerText.text = remain.ToString("F1") + " s";

        if (remain <= 0f)
        {
            memorizing = false;
            StartCoroutine(EndMemorizeAndStartGame());
        }
    }

    System.Collections.IEnumerator EndMemorizeAndStartGame()
{
    // Blend kamera ke player
    SetPriority(overviewHigh: false);

    // Fade global light → gelap
    if (globalLight)
    {
        float t = 0f;
        float from = globalLight.intensity;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            globalLight.intensity = Mathf.Lerp(from, darkIntensity, t / fadeTime);
            yield return null;
        }
        globalLight.intensity = darkIntensity;
    }

    // Nyalakan lampu player
    if (playerLight) playerLight.enabled = true;

    // UI switch
    if (bottomUI) bottomUI.SetActive(true);           // munculkan kontrol
    if (memorizeTimeUI) memorizeTimeUI.SetActive(false); // sembunyikan timer

    // Aktifkan player
    SetPlayerCanMove(true);
}

    void SetPriority(bool overviewHigh)
    {
        if (vcamOverview) vcamOverview.Priority = overviewHigh ? 20 : 0;
        if (vcamPlayer)   vcamPlayer.Priority   = overviewHigh ? 10 : 30;
    }

    void SetPlayerCanMove(bool canMove)
    {
        if (playerController == null) return;
        var t = playerController.GetType();
        var f = t.GetField(canMoveFieldName);
        if (f != null && f.FieldType == typeof(bool)) f.SetValue(playerController, canMove);
    }
}
