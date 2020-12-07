using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_trap : MonoBehaviour
{
    [SerializeField] public Transform firepoint;
    [SerializeField] public GameObject arrow;
     float timebetween;
    [SerializeField] public float startTimeBetween;

    // Start is called before the first frame update
    void Start()
    {
        timebetween = startTimeBetween;
    }

    // Update is called once per frame
    void Update()
    {
        if(timebetween <= 0)
        {
            Instantiate(arrow, firepoint.position, firepoint.rotation);
            timebetween = startTimeBetween;
        }
        else
        {
            timebetween -= Time.deltaTime;
        }
        
    }
}
