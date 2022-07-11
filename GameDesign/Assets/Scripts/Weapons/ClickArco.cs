using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Events;


public class ClickArco : MonoBehaviour
{
    // Start is called before the first frame update
    public WeaponInventory wi;

    public void FixedUpdate()
    {
        wi = GameObject.FindObjectOfType<WeaponInventory>();
    }
    public void TaskOnClickArco()
    {
        Debug.Log("CLICKED THE ARCO BUTTON");

        if (wi.weapons[3].activeInHierarchy)
        {
            wi.setWeaponActive(0);
            Debug.Log("DEACTIVATE ARCO");
            MainManager.Instance.currentWeapon = 0;
        }
        else
        {
            wi.setWeaponActive(3);
            Debug.Log("ACTIVATE ARCO");
            MainManager.Instance.currentWeapon = 3;
        }
    }

}
