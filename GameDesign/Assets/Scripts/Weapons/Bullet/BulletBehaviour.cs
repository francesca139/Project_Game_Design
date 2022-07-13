using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//on the bullet
public class BulletBehaviour : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        //    Destroy(gameObject);
    }
}

