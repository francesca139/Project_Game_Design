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

    //per lo spawn
    public GameObject elixirPrefab;

    public Slider enemyHb;


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

        if (go.tag == "Arrow")
            TakeDamage(MainManager.Instance.bulletDamage);

        if (go.tag == "Mazza")
            TakeDamage(MainManager.Instance.batDamage);

    }

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;

        enemyHb.value = currentHealth;

        if (currentHealth <= 0)
        {   
            makeDead();
        }
    }

    public void makeDead()
    {
        MainManager.Instance.collected.Add(gameObject.name);
        MainManager.Instance.collected.Add(enemyHb.name);

        //per lo spawn
        GameObject spawned = Instantiate(elixirPrefab, transform.position, Quaternion.identity);
        spawned.name = "elisir" + MainManager.Instance.numberOfElixirs.ToString();

        Debug.Log("Enemy Killed!!");

        Destroy(gameObject.transform.root.gameObject);
    }

}