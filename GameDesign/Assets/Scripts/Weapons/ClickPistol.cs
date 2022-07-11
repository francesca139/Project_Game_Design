using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Events;


public class ClickPistol : MonoBehaviour
{
    // Start is called before the first frame update
    public WeaponInventory wi;

    public void FixedUpdate()
    {
        wi = GameObject.FindObjectOfType<WeaponInventory>();
    }
    public void TaskOnClickPistol()
    {
        Debug.Log("CLICKED THE PISTOL BUTTON");

        if (wi.weapons[2].activeInHierarchy)
        {
            wi.setWeaponActive(0);
            Debug.Log("DEACTIVATE PISTOL");
            MainManager.Instance.currentWeapon = 0;
        }
        else
        {
            wi.setWeaponActive(2);
            Debug.Log("ACTIVATE PISTOL");
            MainManager.Instance.currentWeapon = 2;
        }
    }
}
