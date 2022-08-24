using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickPopUpButton : MonoBehaviour
{
    public Button button;

    public void Awake()
    {
        button = GameObject.Find("PopUpButton").GetComponent<Button>();
    }
    public void FixedUpdate()
    {

    }
    public void TaskOnClickPopUp()
    {
        Debug.Log("CLICKED THE POPUP BUTTON");
        MainManager.Instance.popUpActive = false;

        //DISATTIVARE POPUP

    }

}
