using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//nel caso in cui la barra della salute rimanesse fissa nella Ui, tipo in basso

public class HealthUI : MonoBehaviour
{
    //create a canvas in the scene
    public float fullHealth;
    public float currentHealth;


    //SLIDE BAR OF THE HEALTH
    public Slider playerHb;

    bool damaged = false;

    void Start()
    {
        this.currentHealth = this.fullHealth;
        playerHb.maxValue = this.fullHealth;
        playerHb.value = currentHealth;

    }

    void Update()
    {
        damaged = false;
    }

    public void addDamage(float damage) //nello script dei nemici
    {
        this.currentHealth -= damage;

        playerHb.value = this.currentHealth;
        damaged = true;

        if (currentHealth <= 0)
        {
            //the player is dead
            makeDead();
        }
    }

    public void makeDead()
    {   
        Debug.Log("the game obj has been destroyed");
        Destroy(gameObject);
    }
}