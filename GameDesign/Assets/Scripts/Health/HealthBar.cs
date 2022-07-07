using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;

    public Image fill;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }


    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}


//DA SCRIVERE NELLO SCRIPT DELL?OGGETTO SU CUI VIENE POSTA LA BARRA
/*
public int maxHealth = 100;
public int currentHealth;
public HealthBar hBar;
void Start() { 
    currentHealth = maxHealth; 
    hBar.SetMaxHealth(maxHealth);}
void Update() { 
    if (Input.GetKeyDown(KeyCode.Space))
        TakeDamage(20);
}
void TakeDamage(int damage)
{
    currentHealth -= damage;
    hBar.SetHealth(currentHealth);
}

*/
