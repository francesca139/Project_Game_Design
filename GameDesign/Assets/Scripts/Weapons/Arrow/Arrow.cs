using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public string goName;

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory pi = other.GetComponent<PlayerInventory>();

        if (pi != null)
        {
            goName = gameObject.name;
            gameObject.SetActive(false);
            //  Destroy(gameObject);
            MainManager.Instance.collected.Add(gameObject.name);
            pi.ArrowCollected();

        }
    }
}
