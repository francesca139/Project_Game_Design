using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//per raccogliere gli eslir
public class Elixir : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory pi = other.GetComponent<PlayerInventory>();

        if (pi != null)
        {
            pi.ElixirCollected();
            gameObject.SetActive(false);

        }
    }
}
