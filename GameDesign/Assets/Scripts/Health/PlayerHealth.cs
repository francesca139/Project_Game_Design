using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float fullHealth;
    public float currentHealth;

    public Slider playerHb;
    Gradient gradient;
    public Image fill;


    bool damaged = false;


    void Start()
    {
        this.fullHealth = MainManager.Instance.fullHealth;
        this.currentHealth = MainManager.Instance.currentHealth;

        this.gradient = MainManager.Instance.gradientHb;
    }

    private void FixedUpdate()
    {
        this.playerHb = MainManager.Instance.healthSlider;
        this.fill = MainManager.Instance.fillHb;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(1);
        damaged = false;
    }

    public void TakeDamage(float damage) //used in the script of enemies to damage the player
    {
        currentHealth -= damage;

        playerHb.value = MainManager.Instance.currentHealth;
        MainManager.Instance.currentHealth = currentHealth;

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
        //Destroy(gameObject);
    }
}

