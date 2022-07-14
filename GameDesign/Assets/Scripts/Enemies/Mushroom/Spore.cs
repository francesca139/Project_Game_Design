using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore : MonoBehaviour
{
    public float damage;
    public GameObject pl;

    private void Start()
    {
        damage = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hit!!!!!");

            pl = other.gameObject;
            PlayerHealth ph = pl.GetComponent<PlayerHealth>();

            ph.TakeDamage(damage);
        }
    }
}
