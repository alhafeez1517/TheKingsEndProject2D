using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFx : MonoBehaviour
{
        public AudioSource source;
        public AudioClip clickSound;

    void Start()
    {
        
    }

    public void playClickSound()
    {
        source.PlayOneShot(clickSound);
    }
}
