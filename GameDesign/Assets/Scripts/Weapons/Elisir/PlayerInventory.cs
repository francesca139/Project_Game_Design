using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//gestione dell'inventario non delle armi
public class PlayerInventory : MonoBehaviour
{
    public int NumberOfElixirs
    {
        get;
        private set;
    }

    public UnityEvent<PlayerInventory> OnSphereCollected;

    public void ElixirCollected()
    {
        NumberOfElixirs++;
        OnSphereCollected.Invoke(this);
    }

    public void ElixirUsed()
    {
        NumberOfElixirs--;
    }
}
