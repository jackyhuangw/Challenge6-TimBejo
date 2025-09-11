using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<AudioManager>();
    }

    public void MoveToScene(int sceneID)
    {
        audioManager.PlaySFX(audioManager.buttonClickSound);
        SceneManager.LoadScene(sceneID);
    }
   
}
