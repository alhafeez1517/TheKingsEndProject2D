using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt_Player : MonoBehaviour
{
    private AssassinController assassinController;
    private float damageStartTime;
    private bool inTrap = false;

    // Start is called before the first frame update
    void Start()
    {
        assassinController = GameObject.FindGameObjectWithTag("Player").GetComponent<AssassinController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            damageStartTime = Time.realtimeSinceStartup;
            inTrap = true;
            assassinController.HurtPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrap = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (inTrap == true && Time.realtimeSinceStartup - damageStartTime >= 1)
        {
            assassinController.HurtPlayer();
            damageStartTime = Time.realtimeSinceStartup;
        }
    }
}
