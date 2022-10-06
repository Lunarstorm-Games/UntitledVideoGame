using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider; 
    // Start is called before the first frame update
    public void SetMaxMana(float mana) {
        slider.maxValue = mana;
        slider.value = mana;
    }

    public void SetMana(float mana){
        slider.value = mana;
    }
}
