using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponInventory : MonoBehaviour
{
    public List<GameObject> weapons;
    public List<bool> weaponAvailable;

    int currentWeapon;

    //  Vector3 weaponPosition;

    private int countAvailable;

    bool canChange;

    void Start()
    {
        weapons = new List<GameObject>();
        weaponAvailable = new List<bool>();

        //   weaponOnHand.SetActive(true);
        //   weaponPosition = weaponOnHand.transform.position;

        // Commento: lui parte disarmato, quindi si potrebbe modificare la lista weapon come segue
        // - weapon[0] --> disarmato (stato iniziale)
        // - weapon[1] --> bastone 
        // - weapon[2] --> pistola

        // Quando l'utente clicca sulla casella relativa avviene un
        // weapons[1].SetActive(true) e tutti gli altri passano a false,
        // per? pi? che metterlo nello Start bisognerebbe inserirlo in una
        // Update o FixedUpdate, per controllare costantemente il valore
        // del trigger

        //introduco gi? i gameobjects nell'inventario
        GameObject empty = GameObject.FindGameObjectWithTag("EmptyHand");
        weapons.Add(empty);
        weaponAvailable.Add(true);
        weapons[0].SetActive(true);

        GameObject pistola = GameObject.FindGameObjectWithTag("Pistola");
        weapons.Add(pistola);
        weaponAvailable.Add(false);
        weapons[1].SetActive(false);

        GameObject mazza = GameObject.FindGameObjectWithTag("Mazza");
        weapons.Add(mazza);
        weaponAvailable.Add(false);
        weapons[2].SetActive(false); //il personaggio ha in mano la pistola all'inizio

        canChange = false;
        currentWeapon = 0;


    }


    public UnityEvent<WeaponInventory> OnWeaponCollected;
    public void WeaponCollected(GameObject go)

    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].CompareTag(go.name))
                weaponAvailable[i] = true;

        }

        Debug.Log("Another one collected");

        OnWeaponCollected.Invoke(this);

    }

    void Update()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weaponAvailable[i] == true)
                countAvailable++;

        }

        if (countAvailable > 1)
            canChange = true;


        if (canChange && weapons.Count > 2)
        {
            if (Input.GetMouseButtonDown(2))
            {
                Debug.Log("Change weapon");

                int i;
                for (i = currentWeapon + 1; i < weapons.Count; i++)
                {
                    if (weaponAvailable[i] == true)
                    {

                        currentWeapon = i;
                        setWeaponActive(currentWeapon);
                        return;
                    }
                }

                for (i = 0; i < currentWeapon; i++)
                {
                    if (weaponAvailable[i] == true)
                    {
                        currentWeapon = i;
                        setWeaponActive(currentWeapon);
                        return;
                    }
                }

            }

        }

    }

    public void setWeaponActive(int e)
    {
        deactivateWeapons();
        weapons[e].SetActive(true);

    }

    void deactivateWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(false);
        }
    }

    public void activateWeapon(int w)
    {
        weaponAvailable[w] = true;
    }
}