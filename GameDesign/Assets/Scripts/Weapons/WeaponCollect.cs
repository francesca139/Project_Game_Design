using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        WeaponInventory wi = other.GetComponent<WeaponInventory>();

        if (wi != null)
        {
            wi.WeaponCollected(gameObject);

            gameObject.SetActive(false);
          

            Debug.Log("WEAPON COLLECTED!");
        }
    }
}

