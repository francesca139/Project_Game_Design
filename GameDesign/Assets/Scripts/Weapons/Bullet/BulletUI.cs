using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//script nel caso in cui volessimo far comparire il numero dei proiettili raccolti visibile nella scheramat di gioco
public class BulletUI : MonoBehaviour
{
    private PlayerInventory pi;
    private TextMeshProUGUI bulletText;

    void Start()
    {
        bulletText = GetComponent<TextMeshProUGUI>();
        pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        bulletText.text = pi.getNumberOfBullets().ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bulletText.text = pi.getNumberOfBullets().ToString();
    }
}
