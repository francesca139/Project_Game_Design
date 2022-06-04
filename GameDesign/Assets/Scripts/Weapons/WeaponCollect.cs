using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            WeaponInventory wi = other.GetComponent<WeaponInventory>();
            if (wi != null)
            {
                wi.WeaponCollected(gameObject);

                //  gameObject.SetActive(false);
                Destroy(transform.root.gameObject);


                Debug.Log("WEAPON COLLECTED!");
            }
        }
    }
}


