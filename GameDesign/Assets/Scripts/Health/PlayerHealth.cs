using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float fullHealth;
    public float currentHealth;

    public Slider playerHb;
    public Gradient gradient;

    public Image fill;


    bool damaged = false;

    void Start()
    {
        this.currentHealth = this.fullHealth;
        playerHb.maxValue = this.fullHealth;
        playerHb.value = currentHealth;

        fill.color = gradient.Evaluate(1f);

    }

    void Update()
    {
       // if (Input.GetKeyDown(KeyCode.Space))
        //  TakeDamage(20);
        damaged = false;
    }

    public void TakeDamage(float damage) //used in the script of enemies to damage the player
    {
        this.currentHealth -= damage;

        playerHb.value = currentHealth;
        damaged = true;
        fill.color = gradient.Evaluate(playerHb.normalizedValue);

        if (currentHealth <= 0)
        {   //esempio
            makeDead();
        }
    }

    public void makeDead()
    {   
        Debug.Log("the game obj has been destroyed");
        Destroy(gameObject);
    }
}
