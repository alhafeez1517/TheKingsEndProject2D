using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndLevel : MonoBehaviour
{
    public TimeController timeController;
    public LevelComplete levelComplete;
    public GameObject healthCanvas;
    public GameObject timeCanvas;
    public GameObject scoreCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            healthCanvas.gameObject.SetActive(false);
            timeCanvas.gameObject.SetActive(false);
            scoreCanvas.gameObject.SetActive(true);
            timeController.EndTime();
            levelComplete.timeTaken.text = "Total time taken: "+ timeController.timeCounter.text;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
