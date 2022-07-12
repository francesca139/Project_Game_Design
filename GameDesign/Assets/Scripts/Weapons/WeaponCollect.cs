using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollect : MonoBehaviour
{
    private AudioManager audioManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            WeaponInventory wi = other.GetComponent<WeaponInventory>();
            if (wi != null)
            {
                wi.WeaponCollected(gameObject);

                gameObject.SetActive(false);
                MainManager.Instance.collected.Add(gameObject.name);
                //   Destroy(transform.root.gameObject);


                Debug.Log("WEAPON COLLECTED!");
		audioManager = FindObjectOfType<AudioManager>();
		audioManager.SeleccionAudio(0,0.5f);
            }
        }
    }
}



