using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void OpenPausePanel()
    {

        pauseMenu.SetActive(true);
    }

    public void ClosePausePanel()
    {

        pauseMenu.SetActive(false);
    }
    
    public void GoToLevelMenu()
    {
        SceneManager.LoadScene("LevelMenu");
    }
}
