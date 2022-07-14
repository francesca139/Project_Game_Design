using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Events;

public class WeaponInventory : MonoBehaviour
{
    public List<GameObject> weapons;
    public List<bool> weaponAvailable;

    public int currentWeapon;

    private int countAvailable;

    bool canChange;

    public Image batImage;
    public Button batButton;

    public Image pistolImage;
    public Image bulletImage;
    public TextMeshProUGUI bulletText;
    public TextMeshProUGUI xBullet;
    public Button pistolButton;
    public Button bulletButton;
    public Image sottoBullet;

    public Button elixirButton;

    public Image arcoImage;
    public Image arrowImage;
    public TextMeshProUGUI arrowText;
    public TextMeshProUGUI xArrow;
    public Button arcoButton;


    void Start()
    {
        weapons = MainManager.Instance.weapons;
        weaponAvailable = MainManager.Instance.weaponAvailable;

        // Commento: lui parte disarmato, quindi si potrebbe modificare la lista weapon come segue
        // - weapon[0] --> disarmato (stato iniziale)
        // - weapon[1] --> bastone, mondo 1 e 2 
        // - weapon[2] --> pistola, mondo 1
        // -weapon [3] ---> arco, mondo 2

        // Quando l'utente clicca sulla casella relativa avviene un
        // weapons[1].SetActive(true) e tutti gli altri passano a false,
        // per? pi? che metterlo nello Start bisognerebbe inserirlo in una
        // Update o FixedUpdate, per controllare costantemente il valore
        // del trigger

        //introduco gi? i gameobjects nell'inventario
        /*
           weaponAvailable.Add(true);
           weapons[0].SetActive(true);


           batImage.enabled = false;
           // batImage.sprite = batSprite;
           batButton.enabled = false;
           // batButton.SetActive(false);

           pistolImage.enabled = false;
           bulletImage.enabled = false;
           xBullet.enabled = false;
           bulletText.enabled = false;
           //pistolImage.sprite = pistolSprite;
           pistolButton.enabled = false;
           //  pistolButton.enabled = false;

           arcoImage.enabled = false;
           arrowImage.enabled = false;
           xArrow.enabled = false;
           arrowText.enabled = false;

           arcoButton.enabled = false; */

        canChange = MainManager.Instance.canChange;
        // currentWeapon = MainManager.Instance.currentWeapon;


        //batButton2 = MainManager.Instance.batButton;
        //   batButton2.onClick.AddListener(TaskOnClickBat);
        //   UnityEventTools.AddPersistentListener(batButton2.onClick, TaskOnClickBat);



        //    batButton.onClick.AddListener(TaskOnClickBat);
        //   pistolButton.onClick.AddListener(TaskOnClickPistol);
        //   arcoButton.onClick.AddListener(TaskOnClickArco);
    }

    public UnityEvent<WeaponInventory> OnWeaponCollected;

    public void FixedUpdate()
    {
        batButton = MainManager.Instance.batButton;
        batImage = MainManager.Instance.batImage;

        pistolButton = MainManager.Instance.pistolButton;
        pistolImage = MainManager.Instance.pistolImage;
        bulletImage = MainManager.Instance.bulletImage;
        bulletText = MainManager.Instance.bulletText;
        xBullet = MainManager.Instance.xBullet;
        bulletButton = MainManager.Instance.bulletButton;
        sottoBullet = MainManager.Instance.sottoBullet;

        elixirButton = MainManager.Instance.elixirButton;

        arcoButton = MainManager.Instance.arcoButton;
        arcoImage = MainManager.Instance.arcoImage;
        arrowImage = MainManager.Instance.arrowImage;
        arrowText = MainManager.Instance.arrowText;
        xArrow = MainManager.Instance.xArrow;

    }

    public void WeaponCollected(GameObject go)

    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].CompareTag(go.name))
            {
                weaponAvailable[i] = true;
                MainManager.Instance.weaponAvailable[i] = true;
            }

            if (go.name == "Mazza")
            {

                batButton.enabled = true;
                batImage.enabled = true;
                Debug.Log("Bat collected");
            }

            if (go.name == "Pistola")
            {
                pistolButton.enabled = true;
                pistolImage.enabled = true;
                bulletImage.enabled = true;
                xBullet.enabled = true;
                bulletText.enabled = true;

                bulletButton.enabled = true;
                sottoBullet.enabled = true;
             
                elixirButton.enabled = true; //si attiva anche questo con la pistola
                Debug.Log("Pistol collected");
            }

            if (go.name == "Arco")
            {
                arcoButton.enabled = true;
                arcoImage.enabled = true;
                arrowImage.enabled = true;
                xArrow.enabled = true;
                arrowText.enabled = true;
                Debug.Log("Arco collected");
            }



        }

        OnWeaponCollected.Invoke(this);

    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "Fairy")
        {
            currentWeapon = MainManager.Instance.currentWeapon;
            Debug.Log("CHANGE 1");
            if (MainManager.Instance.currentWeapon == 2)
            {
                Debug.Log("CHANGEEEEEEEEEEEEEEEEEEEEEEE");
                setWeaponActive(0);
                currentWeapon = 0;

            }
        }

        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            currentWeapon = MainManager.Instance.currentWeapon;
            Debug.Log("CHANGE 1");
            if (MainManager.Instance.currentWeapon == 3)
            {
                Debug.Log("CHANGEEEEEEEEEEEEEEEEEEEEEEE");
                setWeaponActive(0);
                currentWeapon = 0;

            }
        }
    }

    public void TaskOnClickBat()
    {
        Debug.Log("You have clicked the Batbutton!");

        if (weapons[1].activeInHierarchy)
        { setWeaponActive(0); }
        else
            setWeaponActive(1);
    }

    public void TaskOnClickPistol()
    {
        Debug.Log("You have clicked the Pistolbutton!");

        if (weapons[2].activeInHierarchy)
        {
            setWeaponActive(0);
        }
        else
        {
            setWeaponActive(2);
        }

    }

    public void TaskOnClickArco()
    {
        Debug.Log("You have clicked the Arcobutton!");

        if (weapons[3].activeInHierarchy)
        { setWeaponActive(0); }
        else
            setWeaponActive(3);
    }


    public void setWeaponActive(int e)
    {
        deactivateWeapons();
        weapons[e].SetActive(true);
        MainManager.Instance.weapons[e].SetActive(true);

    }

    void deactivateWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(false);
            MainManager.Instance.weapons[i].SetActive(false);
        }
    }

    public void activateWeapon(int w)
    {
        weaponAvailable[w] = true;
        MainManager.Instance.weaponAvailable[w] = true;

    }
}