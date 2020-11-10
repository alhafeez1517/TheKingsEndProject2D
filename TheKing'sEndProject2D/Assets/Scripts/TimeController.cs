using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
   public Text timeCounter;
    public TimeSpan timeSpan;
    public bool continueTime;
    public float elapsedTime;

    void Start()
    {
        timeCounter.text = "00:00";
        StartTime();
        //continueTime = false;
        
    }

    void StartTime()
    {
        continueTime = true;
        elapsedTime = 0;
        StartCoroutine(UpdateTimer());

    }

    void EndTime()
    {
        continueTime = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (continueTime)
        {
            elapsedTime += Time.deltaTime;
            timeSpan = TimeSpan.FromSeconds(elapsedTime);
            string currentTime = "Time: "+ timeSpan.ToString("mm':'ss");
            timeCounter.text = currentTime;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
