using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
 
  

    public void onPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void onOptionsButton()
    {

    }

     public void onExitButton()
    {
        Application.Quit();
    }
}
