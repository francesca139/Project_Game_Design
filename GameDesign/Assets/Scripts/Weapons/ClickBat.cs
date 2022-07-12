using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Events;

public class ClickBat : MonoBehaviour
{
    public WeaponInventory wi;
    public int currentWeapon;

    public void FixedUpdate()
    {
        wi = GameObject.FindObjectOfType<WeaponInventory>();
    }
    public void TaskOnClickBat()
    {
        Debug.Log("CLICKED THE BAR BUTTON");

        if (wi.weapons[1].activeInHierarchy)
        {
            wi.setWeaponActive(0);
            Debug.Log("DEACTIVATE BAT");
            MainManager.Instance.currentWeapon = 0;
	   
        }
        else
        {
            wi.setWeaponActive(1);
            Debug.Log("ACTIVATE BAT");
            MainManager.Instance.currentWeapon = 1;
	    
        }
    }
}