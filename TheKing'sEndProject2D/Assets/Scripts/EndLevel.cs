using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndLevel : MonoBehaviour
{
    private TimeController timeController;
    private LevelComplete levelComplete;
    private AssassinController assassinController;
    private GameObject healthCanvas;
    private GameObject timeCanvas;
    private GameObject scoreCanvas;

    // Start is called before the first frame update
    void Start()
    {
        assassinController = GameObject.FindGameObjectWithTag("Player").GetComponent<AssassinController>();
        assassinController.GetCurrentHealth();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            healthCanvas.gameObject.SetActive(false);
            timeCanvas.gameObject.SetActive(false);
            scoreCanvas.gameObject.SetActive(true);
            timeController.EndTime();
            //levelComplete.timeTaken.text = "Total time taken: "+ timeController.timeCounter.text;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
