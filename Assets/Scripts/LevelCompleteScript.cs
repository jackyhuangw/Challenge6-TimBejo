using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelCompleteScript : MonoBehaviour
{

    public void OnLevelComplete(int starAquired)
    {
        if (LevelSelectionMenuManager.currLevel == LevelSelectionMenuManager.unlockedLevels)
        {
           
            LevelSelectionMenuManager.unlockedLevels++;
           

            PlayerPrefs.SetInt("unlockedLevels", LevelSelectionMenuManager.unlockedLevels);
        }
        if (starAquired > PlayerPrefs.GetInt("stars" + LevelSelectionMenuManager.currLevel.ToString(), 0))
        {
            PlayerPrefs.SetInt("stars" + LevelSelectionMenuManager.currLevel.ToString(), starAquired);
        }
        SceneManager.LoadScene("LevelMenu");
    }
}
