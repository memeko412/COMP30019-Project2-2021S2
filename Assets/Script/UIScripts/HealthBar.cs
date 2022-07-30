using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image filler;
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        filler.color = gradient.Evaluate(1f);
    }
    public void SetHealth(float health)
    {
        slider.value = health;
        filler.color = gradient.Evaluate(slider.normalizedValue);
    }
}
