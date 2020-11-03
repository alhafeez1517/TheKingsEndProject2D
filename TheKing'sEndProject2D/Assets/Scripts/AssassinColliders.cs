using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinColliders : MonoBehaviour
{

    private bool grounded;
    private bool sliding;

    // Start is called before the first frame update
    void Start()
    {
        grounded = false;
        sliding = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GroundedState()
    {
        return grounded;
    }

    public bool SlidingState()
    {
        return sliding;
    }   

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground") 
        {
            grounded = true;
        }

        if (other.tag  == "Wall")
        {
            sliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Ground")
        {
            grounded = false;
        }

        if (other.tag == "Wall")
        {
            sliding = false;
        }
    }
}
