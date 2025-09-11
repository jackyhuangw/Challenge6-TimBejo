using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip buttonClickSound;

    public AudioSource musicRef, sfxRef;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Start background music only once
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
        else
        {
            // Destroy duplicates
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
       
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
       
    }
}
