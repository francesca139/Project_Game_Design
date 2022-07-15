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
    public Button button;

    public Animator myAnim;

    public void Awake()
    {
        button = GameObject.Find("UIPistola").GetComponent<Button>();
        myAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    public void FixedUpdate()
    {
        wi = GameObject.FindObjectOfType<WeaponInventory>();

        ColorBlock cb = button.colors;

        if (MainManager.Instance.currentWeapon == 2)
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
    public void TaskOnClickPistol()
    {
        Debug.Log("CLICKED THE PISTOL BUTTON");

        if (wi.weapons[2].activeInHierarchy)
        {
            wi.setWeaponActive(0);
            Debug.Log("DEACTIVATE PISTOL");
            MainManager.Instance.currentWeapon = 0;

            myAnim.SetLayerWeight(2, 0);
            MainManager.Instance.animationLayer = 0;
        }
        else
        {
            wi.setWeaponActive(2);
            Debug.Log("ACTIVATE PISTOL");
            MainManager.Instance.currentWeapon = 2;

            myAnim.SetLayerWeight(2, 1);
            MainManager.Instance.animationLayer = 2;
        }
    }
}
