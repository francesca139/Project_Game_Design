using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//gestione dell'inventario non delle armi
public class PlayerInventory : MonoBehaviour
{
    public int numberOfPeople;
    public int numberOfElixirs;
    public int numberOfArrows;

    public void Start()
    {
        numberOfPeople = 0;
        numberOfElixirs = 0;
        numberOfArrows = 0;
    }

    public UnityEvent<PlayerInventory> OnElixirCollected;
    public UnityEvent<PlayerInventory> OnArrowCollected;
    public UnityEvent<PlayerInventory> OnPersonSaved;

    public void ElixirCollected()
    {
        MainManager.Instance.numberOfElixirs++;
        this.numberOfElixirs = MainManager.Instance.numberOfElixirs;


        OnElixirCollected.Invoke(this);
    }

    public void ElixirUsed()
    {
        MainManager.Instance.numberOfElixirs--;
        this.numberOfElixirs = MainManager.Instance.numberOfElixirs;
    }

    public void setNumberOfElixirs(int i)
    {
        this.numberOfElixirs = i;
    }

    public int getNumberOfElixirs()
    {
        return this.numberOfElixirs;
    }

    public void ArrowCollected()
    {
        MainManager.Instance.numberOfArrows++;
        this.numberOfArrows = MainManager.Instance.numberOfArrows;

        OnArrowCollected.Invoke(this);
    }

    public void ArrowUsed()
    {
        MainManager.Instance.numberOfArrows--;
        setNumberOfArrows(MainManager.Instance.numberOfArrows);
    }

    public int getNumberOfArrows()
    {
        return this.numberOfArrows;
    }

    public void setNumberOfArrows(int i)
    {
        this.numberOfArrows = i;
    }


    public void PersonSaved()
    {
        MainManager.Instance.numberOfPeople++;
        setNumberOfPeople(MainManager.Instance.numberOfPeople);

        OnPersonSaved.Invoke(this);
    }

    public int getNumberOfPeople()
    {
        return this.numberOfPeople;
    }

    void setNumberOfPeople(int n)
    {
        this.numberOfPeople = n;
    }
}