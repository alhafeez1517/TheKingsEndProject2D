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

    // Need to change to tags not names

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "GroundCollider") // name of the ground's collider
        {
            grounded = true;
        }

        if (other.name == "WallCollider")
        {
            sliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.name == "GroundCollider")
        {
            grounded = false;
        }

        if (other.name == "WallCollider")
        {
            sliding = false;
        }
    }
}
