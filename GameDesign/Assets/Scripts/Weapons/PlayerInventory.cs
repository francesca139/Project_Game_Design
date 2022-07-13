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
    public int numberOfBullets;

    public void Start()
    {
        numberOfPeople = MainManager.Instance.numberOfPeople;
        numberOfElixirs = MainManager.Instance.numberOfElixirs;
        numberOfArrows = MainManager.Instance.numberOfArrows;
        numberOfBullets = MainManager.Instance.numberOfBullets;
    }

    public UnityEvent<PlayerInventory> OnElixirCollected;
    public UnityEvent<PlayerInventory> OnArrowCollected;
    public UnityEvent<PlayerInventory> OnBulletCollected;
    public UnityEvent<PlayerInventory> OnPersonSaved;

    public void ElixirCollected()
    {
        this.numberOfElixirs++;
        MainManager.Instance.numberOfElixirs = this.numberOfElixirs;


        OnElixirCollected.Invoke(this);
    }

    public void ElixirUsed()
    {
        this.numberOfElixirs--;
        MainManager.Instance.numberOfElixirs = this.numberOfElixirs;
    }

    public void setNumberOfElixirs(int i)
    {
        this.numberOfElixirs = i;
        MainManager.Instance.numberOfElixirs = this.numberOfElixirs;
    }

    public int getNumberOfElixirs()
    {
        return this.numberOfElixirs;
    }

    public void BulletCollected()
    {
        this.numberOfBullets++;
        MainManager.Instance.numberOfBullets = this.numberOfBullets;


        OnElixirCollected.Invoke(this);
    }

    public void BulletUsed()
    {
        this.numberOfBullets--;
        MainManager.Instance.numberOfBullets = this.numberOfBullets;
    }

    public void setNumberOfBullets(int i)
    {
        this.numberOfBullets = i;
        MainManager.Instance.numberOfBullets = this.numberOfBullets;
    }

    public int getNumberOfBullets()
    {
        return this.numberOfBullets;
    }

    public void ArrowCollected()
    {
        this.numberOfArrows++;
        MainManager.Instance.numberOfArrows = this.numberOfArrows;

        OnArrowCollected.Invoke(this);
    }

    public void ArrowUsed()
    {
        numberOfArrows--;
        MainManager.Instance.numberOfArrows = this.numberOfArrows;
    }

    public int getNumberOfArrows()
    {
        return this.numberOfArrows;
    }

    public void setNumberOfArrows(int i)
    {
        this.numberOfArrows = i;
        MainManager.Instance.numberOfArrows = this.numberOfArrows;
    }


    public void PersonSaved()
    {
        this.numberOfPeople++;
        MainManager.Instance.numberOfPeople = this.numberOfPeople;

        OnPersonSaved.Invoke(this);
    }

    public int getNumberOfPeople()
    {
        return this.numberOfPeople;
    }

    void setNumberOfPeople(int n)
    {
        this.numberOfPeople = n;
        MainManager.Instance.numberOfPeople = this.numberOfPeople;
    }
}