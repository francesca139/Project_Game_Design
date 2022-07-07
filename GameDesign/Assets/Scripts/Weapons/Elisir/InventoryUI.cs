using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//script nel caso in cui volessimo far comparire il numero degli elisir raccolti visibile nella scheramat di gioco
public class InventoryUI : MonoBehaviour
{
    private PlayerInventory pi;
    private TextMeshProUGUI elixirText;

    void Start()
    {
        elixirText = GetComponent<TextMeshProUGUI>();
       pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        elixirText.text = pi.NumberOfElixirs.ToString();
    }

    // Update is called once per frame
    public void UpdateBulletText(PlayerInventory pi)
    {
        elixirText.text = pi.NumberOfElixirs.ToString();
    }
}