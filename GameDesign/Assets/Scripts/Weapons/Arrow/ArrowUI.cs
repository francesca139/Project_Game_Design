using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowUI : MonoBehaviour
{
    private PlayerInventory pi;
    private TextMeshProUGUI arrowText;

    void Start()
    {
        arrowText = GetComponent<TextMeshProUGUI>();
        pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        arrowText.text = pi.getNumberOfArrows().ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        arrowText.text = pi.getNumberOfArrows().ToString();
    }
}