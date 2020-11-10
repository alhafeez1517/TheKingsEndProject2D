using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Slider healthSlider;

    void SetHealth(float health)
    {
        healthSlider.value = health;
    }

    
}
