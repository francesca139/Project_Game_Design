using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour
{
    public PopUpSystem pop;
    public bool detected;

    public void Awake()
    {
        pop = GameObject.Find("MainManager").GetComponent<PopUpSystem>();
        detected = false;
    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            MainManager.Instance.collected.Add(gameObject.name);
            MainManager.Instance.popUpActive = true;
            detected = true;
            pop.PopUp("Hi");
        }
    }
}
