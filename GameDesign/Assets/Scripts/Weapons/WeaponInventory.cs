using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponInventory : MonoBehaviour
{
    public List<GameObject> weapons;
    List<bool> weaponAvailable;
    public GameObject weaponOnHand;

    int currentWeapon;
    
    Vector3 weaponPosition;

    bool canChange;

    void Start()
    {
        weapons = new List<GameObject>();
        weaponAvailable = new List<bool>();

        weaponOnHand.SetActive(false);
        weaponPosition = weaponOnHand.transform.position;

        //dal momento che il personaggio ha già in mano la pistola
        GameObject pistola = GameObject.FindGameObjectWithTag("Pistola");
        weapons.Add(pistola);
        weaponAvailable.Add(true);

        canChange = false;
        currentWeapon = 0;
     

    }

    

    public UnityEvent<WeaponInventory> OnWeaponCollected;
    public void WeaponCollected(GameObject go)
    {    
        bool already = false;
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].name == go.name)
                already = true;
        }

        if (!already)
        {
            if(go.tag == "Mazza")
                 go.GetComponent<BatMovement>().enabled = false;

            weapons.Add(go);
            weaponAvailable.Add(true);

            Debug.Log("Another one collected");

            OnWeaponCollected.Invoke(this);
        }
    }

    void Update()
    {
         weaponPosition = weaponOnHand.transform.position;
        if (weapons.Count > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                weaponOnHand.SetActive(true);
                weapons[currentWeapon].SetActive(true);
                weaponPosition = weaponOnHand.transform.position;
                weapons[currentWeapon].transform.position = weaponOnHand.transform.position;

                canChange = true;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                weaponOnHand.SetActive(false);
                weapons[currentWeapon].SetActive(false);
                canChange = false;
            }
        }

        if (canChange && weapons.Count > 1)
        {
            //  if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f)
            if(Input.GetMouseButtonDown(2))
            {
                Debug.Log("Change weapon");

                int i;
                for (i = currentWeapon + 1; i < weapons.Count; i++)
                {
                    if (weaponAvailable[i] == true)
                    {

                        currentWeapon = i;
                        setWeaponActive(currentWeapon);
                        weapons[currentWeapon].transform.position = weaponPosition;

                        return;
                    }
                }

                for (i = 0; i < currentWeapon; i++)
                {
                    if (weaponAvailable[i] == true)
                    {
                        currentWeapon = i;
                        setWeaponActive(currentWeapon);
                        weapons[currentWeapon].transform.position = weaponPosition;

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