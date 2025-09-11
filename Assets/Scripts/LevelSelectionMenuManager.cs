using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionMenuManager : MonoBehaviour
{
    public LevelObject[] levelObjects;

    public Sprite goldenStarSprite;

    public static int currLevel;
    public static int unlockedLevels;

     AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<AudioManager>();
    }

    public void OnClickLevel(int level)
    {
        audioManager.PlaySFX(audioManager.buttonClickSound);
        currLevel = level;
        SceneManager.LoadScene("ValenScene");

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unlockedLevels = PlayerPrefs.GetInt("unlockedLevels", 0);
        print("unlocked level " + unlockedLevels);
        for (int i = 0; i < levelObjects.Length; i++)
        {
            if (unlockedLevels >= i)
            {
                levelObjects[i].levelButton.interactable = true;
                int stars = PlayerPrefs.GetInt("stars" + i.ToString(), 0);
                for (int j = 0; j < stars; j++)
                {
                    levelObjects[i].stars[j].sprite = goldenStarSprite;
                }  
            }
        }
    }

   
}
