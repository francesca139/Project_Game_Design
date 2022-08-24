using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomJump : MonoBehaviour
{
    public bool jumped;
    public bool firstJump;
    public bool exited;

    // Start is called before the first frame update
    void Start()
    {
        jumped = false;
        firstJump = true;
        exited = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jumped = true;
            firstJump = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        jumped = false;
    }


}
