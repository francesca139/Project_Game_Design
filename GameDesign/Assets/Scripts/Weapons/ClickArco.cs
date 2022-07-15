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
    public Button button;

    public Animator myAnim;

    public void Awake()
    {
        button = GameObject.Find("UIArco").GetComponent<Button>();
        myAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    public void FixedUpdate()
    {
        wi = GameObject.FindObjectOfType<WeaponInventory>();

        ColorBlock cb = button.colors;

        if (MainManager.Instance.currentWeapon == 3)
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
    public void TaskOnClickArco()
    {
        Debug.Log("CLICKED THE ARCO BUTTON");

        if (wi.weapons[3].activeInHierarchy)
        {
            wi.setWeaponActive(0);
            Debug.Log("DEACTIVATE ARCO");
            MainManager.Instance.currentWeapon = 0;

            myAnim.SetLayerWeight(3, 0);
            MainManager.Instance.animationLayer = 0;

        }
        else
        {
            wi.setWeaponActive(3);
            Debug.Log("ACTIVATE ARCO");
            MainManager.Instance.currentWeapon = 3;

           
            myAnim.SetLayerWeight(3, 1);
            MainManager.Instance.animationLayer = 3;
        }
    }

}
