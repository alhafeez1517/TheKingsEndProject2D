using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRewindController : MonoBehaviour
{
    public Slider timeRewindSlider;

    public void SetMaxMana(float mana)
    {
        timeRewindSlider.maxValue = mana;
        timeRewindSlider.value = mana;
    }
    public void SetMana(float mana)
    {
        timeRewindSlider.value = mana;
    }
}
