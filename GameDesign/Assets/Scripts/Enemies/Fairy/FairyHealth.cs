using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FairyHealth : MonoBehaviour
{
    public float fullHealth;
    public float currentHealth;
    public GameObject go;
    public PlayerHealth ph;

    public bool detected;

    public Slider enemyHb;

    private Coroutine co = null;

    void Start()
    {
        this.fullHealth = 5;
        this.currentHealth = fullHealth;

        enemyHb.maxValue = fullHealth;
        enemyHb.value = fullHealth;

        detected = false;
    }

    private void FixedUpdate()
    {
        if (!detected && co != null)
        {
            StopCoroutine(co);
            Debug.Log("Stopped");
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        go = other.gameObject;

        if (go.tag == "Arrow")
            TakeDamage(MainManager.Instance.bulletDamage);

        if (go.tag == "Mazza")
            TakeDamage(MainManager.Instance.batDamage);

        if (go.tag == "Player")
        {
            ph = go.GetComponent<PlayerHealth>();

            detected = true;
            co = StartCoroutine(DamagePlayer(ph));
        }

    }

    private IEnumerator DamagePlayer(PlayerHealth ph)
    {
        var wait = new WaitForSeconds(1);

        while (detected)
        {
            yield return wait;
            ph.TakeDamage(1f);
            Debug.Log("damage");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            detected = false;
        }
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

        Debug.Log("Enemy Killed!!");

        Destroy(gameObject.transform.root.gameObject);
    }
}
