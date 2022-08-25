using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanHealth : MonoBehaviour
{
  
    public GameObject go;
    public PlayerHealthLisa ph;
    public HumanController hc;

    public bool detected;
    public bool first;

    private Coroutine co = null;


    void Start()
    {
      /*  this.fullHealth = 5;
        this.currentHealth = fullHealth;

        enemyHb.maxValue = fullHealth;
        enemyHb.value = fullHealth;  */
        first = true;

        detected = false;

    }

    private void FixedUpdate()
    {
        if (!detected && co != null)
        {
            // StopCoroutine(DamagePlayer(ph));


            StopCoroutine(co);
            Debug.Log("Stopped");
            first = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        go = other.gameObject;

        if (go.tag == "Bullet")
            makeDead();

        if (go.tag == "Mazza")
            TakeDamage(MainManager.Instance.batDamage);

        if (go.tag == "Elixir")
            //  TakeDamage(MainManager.Instance.elixirEffect);
            makeSaved();



        if (go.tag == "Player")
        {
            ph = go.GetComponent<PlayerHealthLisa>();

            detected = true;
            co = StartCoroutine(DamagePlayer(ph));
        }

    }

    private IEnumerator DamagePlayer(PlayerHealthLisa ph)
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
      //  hc.damaged();
    }

    public void makeSaved()
    {
       
        MainManager.Instance.numberOfPeople++;
        hc.Saved();
    }

    public void makeDead()
    {
        MainManager.Instance.collected.Add(gameObject.name);

        Debug.Log("Human Killed!!");

        Destroy(gameObject.transform.root.gameObject);
    }
}
