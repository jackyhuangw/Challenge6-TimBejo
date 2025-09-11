using UnityEngine;

public class SoundMenu : MonoBehaviour
{
    [SerializeField] GameObject soundMenu;

    public void OpenSoundSettings()
    {
       
        soundMenu.SetActive(true);
    }

    public void CloseSoundSettings()
    {
       
        soundMenu.SetActive(false);
    }
}
