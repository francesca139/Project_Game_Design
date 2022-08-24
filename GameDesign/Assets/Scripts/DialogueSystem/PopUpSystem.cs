using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpSystem : MonoBehaviour
{
    public Image popUpBoxImage;
    public Button popUpButton;
    public Image popUpButtonImage;
    public TMP_Text popUpText;

    // public Animator animator;


    public void FixedUpdate()
    {
        popUpBoxImage = MainManager.Instance.popUpBoxImage;
        popUpButton = MainManager.Instance.popUpButton;
        popUpButtonImage = MainManager.Instance.popUpButtonImage;
        popUpText = MainManager.Instance.popUpText;

        //SOLO PER IL MOMENTO
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            if (MainManager.Instance.popUpActive)
            {

                popUpBoxImage.enabled = true;
                popUpText.enabled = true;
                popUpButton.enabled = true;
                popUpButtonImage.enabled = true;
            }
            else
            {
                //  popUpBox.SetActive(false);
                popUpBoxImage.enabled = false;
                popUpText.enabled = false;
                popUpButton.enabled = false;
                popUpButtonImage.enabled = false;
            }
        }
    }

    public void PopUp(string text)
    {
        //  MainManager.Instance.popUpActive = true;
        //   popUpBox.SetActive(MainManager.Instance.popUpActive);
        popUpText.text = text;
        //   animator.SetTrigger("pop");
    }


}

