using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelComplete : MonoBehaviour
{
    #region Texts
    public TMP_Text levelComplete;
    public TMP_Text timesDied;
    public TMP_Text enemieskilled;
    public TMP_Text timeTaken;
    #endregion

    string sceneName;
    int noOfDeaths;

    #region Buttons
    public Button mainMenuButton;
    public Button nextLevelButton;
    #endregion

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(sceneName);
    }

    public void getNoOfDeaths(int deaths)
    {
        noOfDeaths = deaths;
        timesDied.text = "Total Rewinds: " + noOfDeaths;
    }
    
    void Update()
    {
        

        if (sceneName == "Level_3")
        {
            nextLevelButton.gameObject.SetActive(false);
        }

        

    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void OnNextLevelClick()
    {
        switch (sceneName)
        {
            case "Level_1":
                SceneManager.LoadScene("Level_2");
                break;
            case "Level_2":
                SceneManager.LoadScene("Level_3");
                break;
              
        }
    }
}
