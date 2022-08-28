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
    public Animator myAnim;

    public WeaponInventory wi;
    public int currentWeapon;
    public bool active;

    Button button;

    public void Awake()
    {
        button = GameObject.Find("UIMazza").GetComponent<Button>();
        myAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        active = false;
    }

    public void FixedUpdate()
    {
        wi = GameObject.FindObjectOfType<WeaponInventory>();

        ColorBlock cb = button.colors;

        if (MainManager.Instance.currentWeapon == 1)
        {
            cb.colorMultiplier = 5;
            button.colors = cb;

        }
        else
        {
            cb.colorMultiplier = 1;
            button.colors = cb;

        }

    }

    public void Update()
    {
        if (active)
        {
            if (Input.GetMouseButtonDown(0))
                Debug.Log("Colpisci");
        }
    }
    public void TaskOnClickBat()
    {
        Debug.Log("CLICKED THE BAR BUTTON");

        if (wi.weapons[1].activeInHierarchy)
        {
            wi.setWeaponActive(0);
            Debug.Log("DEACTIVATE BAT");
            MainManager.Instance.currentWeapon = 0;

            // myAnim.SetLayerWeight(1, 1);
            myAnim.SetLayerWeight(1, 0);
            MainManager.Instance.animationLayer = 0;

            active = false;

        }
        else
        {
            wi.setWeaponActive(1);
            Debug.Log("ACTIVATE BAT");
            MainManager.Instance.currentWeapon = 1;

            myAnim.SetLayerWeight(1, 1);
            MainManager.Instance.animationLayer = 1;

            active = true;

        }
    }
}