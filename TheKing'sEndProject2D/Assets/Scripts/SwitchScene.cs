using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SwitchScene : MonoBehaviour
{
    #region Buttons
    public Button playButton;
    public Button optionsButton;
    public Button instructionsButton;
    public Button quitButton;
    public Button backButtonInstruct;
    public Button backButtonOptions;
    public Button wButton;
    public Button aButton;
    public Button dButton;
    public Button lMBButton;
    public Button rMBButton;
    public Button shiftButton;
    public Button spaceButton;
    #endregion
    [Space(10)]
    #region Animations
    public GameObject run;
    public GameObject jump;
    public GameObject roll;
    public GameObject attack;
    public GameObject block;
    public GameObject rewind;
    #endregion
    [Space(10)]
    #region Texts
    public TMP_Text gameTitle;
    public TMP_Text gameTitleInstruct;
    #endregion




    public void onPlayButton()
    {

        Invoke("onPlayClick", 0.6f);
    }

    public void onInstructionClick()
    {
        Invoke("onInstructionsButton", 0.6f);
    }

    public void onBackButtonClick()
    {
        Invoke("onBackButtonInstruct", 0.6f);
    }

    public void onPlayClick()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void onOptionsButton()
    {

    }

    public void onBackButtonInstruct()
    {
        playButton.gameObject.SetActive(true);
        optionsButton.gameObject.SetActive(true);
        instructionsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        backButtonInstruct.gameObject.SetActive(false);
        gameTitle.gameObject.SetActive(true);

        gameTitleInstruct.gameObject.SetActive(false);
        wButton.gameObject.SetActive(false);
        aButton.gameObject.SetActive(false);
        dButton.gameObject.SetActive(false);
        lMBButton.gameObject.SetActive(false);
        rMBButton.gameObject.SetActive(false);
        shiftButton.gameObject.SetActive(false);
        spaceButton.gameObject.SetActive(false);

        run.gameObject.SetActive(false);
        jump.gameObject.SetActive(false);
        roll.gameObject.SetActive(false);
        attack.gameObject.SetActive(false);
        block.gameObject.SetActive(false);
        rewind.gameObject.SetActive(false);
    }

    public void onInstructionsButton()
    {
        playButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        instructionsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        backButtonInstruct.gameObject.SetActive(true);
        gameTitle.gameObject.SetActive(false);

        gameTitleInstruct.gameObject.SetActive(true);
        wButton.gameObject.SetActive(true);
        aButton.gameObject.SetActive(true);
        dButton.gameObject.SetActive(true);
        lMBButton.gameObject.SetActive(true);
        rMBButton.gameObject.SetActive(true);
        shiftButton.gameObject.SetActive(true);
        spaceButton.gameObject.SetActive(true);

        run.gameObject.SetActive(true);
        jump.gameObject.SetActive(true);
        roll.gameObject.SetActive(true);
        attack.gameObject.SetActive(true);
        block.gameObject.SetActive(true);
        rewind.gameObject.SetActive(true);


    }

     public void onExitButton()
    {
        Application.Quit();
    }
}
