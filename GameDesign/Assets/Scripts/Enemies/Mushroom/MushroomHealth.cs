using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MushroomHealth : MonoBehaviour
{
    public float fullHealth;
    public float currentHealth;
    public GameObject go;

    public Slider enemyHb;

    bool damaged;

    void Start()
    {
        this.fullHealth = 5;
        this.currentHealth = fullHealth;

        enemyHb.maxValue = fullHealth;
        enemyHb.value = fullHealth;
    }


    private void OnTriggerEnter(Collider other)
    {

        go = other.gameObject;

        if (go.tag == "Bullet")
            TakeDamage(MainManager.Instance.bulletDamage);

    }

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;

        enemyHb.value = currentHealth;

        damaged = true;

        if (currentHealth <= 0)
        {   
            makeDead();
        }
    }

    public void makeDead()
    {
        MainManager.Instance.collected.Add(gameObject.name);
        MainManager.Instance.collected.Add(enemyHb.name);

        Debug.Log("Enemy Killed!!");

        Destroy(gameObject.transform.root.gameObject);
    }

}