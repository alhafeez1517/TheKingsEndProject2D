using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndLevel : MonoBehaviour
{
    private TimeController timeController;
    public AudioSource audioSource;
    public AudioClip niceSound;
    private LevelComplete levelComplete;
    //private AssassinController assassinController;
    private GameObject assassin;
    private GameObject healthCanvas;
    private GameObject timeCanvas;
    public GameObject scoreCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
        assassin = GameObject.FindGameObjectWithTag("Player");
        timeController = GameObject.Find("Time").GetComponent<TimeController>();
       levelComplete = GameObject.Find("ScoreController").GetComponent<LevelComplete>();
        healthCanvas = GameObject.Find("HealthBar");
        timeCanvas = GameObject.Find("Manabar");
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(niceSound);
      
            assassin.gameObject.SetActive(false);
            healthCanvas.gameObject.SetActive(false);
            timeCanvas.gameObject.SetActive(false);
            scoreCanvas.gameObject.SetActive(true);
            timeController.EndTime();
            levelComplete.timeTaken.text = "Total time taken: " + timeController.currentTime;
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
