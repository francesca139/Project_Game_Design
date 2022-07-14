using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickElixir : MonoBehaviour
{
    public Button button;

    public void Awake()
    {
        button = GameObject.Find("InfoElixirs").GetComponent<Button>();
    }
    public void FixedUpdate()
    {

        ColorBlock cb = button.colors;

        if (MainManager.Instance.currentWeapon == 2)
        {
            if (MainManager.Instance.typeProiettile == 1)
            {
                cb.colorMultiplier = 1.5f;
                button.colors = cb;
            }
            else if (MainManager.Instance.typeProiettile == 0)
            {
                cb.colorMultiplier = 1;
                button.colors = cb;
            }
        }

    }

    // Update is called once per frame
    public void TaskOnClickElixirs()
    {
        if (MainManager.Instance.currentWeapon == 2)
        {
            Debug.Log("CLICKED THE Elixirs BUTTON");

            if (MainManager.Instance.typeProiettile == 0)
            {
                Debug.Log("Use elixirs!");
                MainManager.Instance.typeProiettile = 1;
            }
            else if (MainManager.Instance.typeProiettile == 1)
            {
                Debug.Log("Use bullets!");
                MainManager.Instance.typeProiettile = 0;
            }
        }
    }
}
