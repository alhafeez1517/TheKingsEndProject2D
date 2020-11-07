using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Animation : MonoBehaviour
{
    private Animator anim;
    private bool isSpikeOut = false;
    [SerializeField] float spikeOut_delay = 1f;
    [SerializeField] float spikeIn_delay = 1f;
    [SerializeField] float spike_speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpikeOut == false)
        {
            isSpikeOut = true;
            Invoke("SpikeOut", spikeOut_delay);
        }

    }

    void SpikeOut()
    {
        anim.speed = spike_speed;
        anim.SetBool("spikeUp", true);
        Invoke("SpikeIn", spikeIn_delay);
    }

    void SpikeIn()
    {
        anim.speed = spike_speed;
        anim.SetBool("spikeUp", false);
        anim.SetBool("spikeDown", true);
        Invoke("SpikeIdle", spikeOut_delay);
    }

    void SpikeIdle()
    {
        anim.SetBool("spikeDown", false);
        isSpikeOut = false;
    }

}
