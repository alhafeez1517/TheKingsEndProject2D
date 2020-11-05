using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
   
   

    public void onPlayButton()
    {

        Invoke("onPlayClick", 0.6f);
    }

    public void onPlayClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void onOptionsButton()
    {

    }

     public void onExitButton()
    {
        Application.Quit();
    }
}
