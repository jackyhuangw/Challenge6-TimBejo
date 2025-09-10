using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject[] levels;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levels[LevelSelectionMenuManager.currLevel].SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
