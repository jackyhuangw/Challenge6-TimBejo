using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    private void Start()
    {
        // Load saved slider values or set default values
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        _sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Apply the loaded values
        AudioManager.Instance.MusicVolume(_musicSlider.value);
        AudioManager.Instance.SFXVolume(_sfxSlider.value);

        // Add listeners to save values when sliders are changed
        _musicSlider.onValueChanged.AddListener(delegate { SaveMusicVolume(); });
        _sfxSlider.onValueChanged.AddListener(delegate { SaveSFXVolume(); });
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }

    private void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
        PlayerPrefs.Save();
    }

    private void SaveSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVolume", _sfxSlider.value);
        PlayerPrefs.Save();
    }
}
