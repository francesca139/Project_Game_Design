using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PeopleUI : MonoBehaviour
{
    private PlayerInventory pi;
    public TextMeshProUGUI peopleText;

    void Start()
    {
        peopleText = GetComponent<TextMeshProUGUI>();
        pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        peopleText.text = pi.getNumberOfPeople().ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        peopleText.text = pi.getNumberOfPeople().ToString();
    }
}
