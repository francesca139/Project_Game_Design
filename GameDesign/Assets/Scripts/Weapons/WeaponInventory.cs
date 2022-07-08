using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class WeaponInventory : MonoBehaviour
{
    public List<GameObject> weapons;
    public List<bool> weaponAvailable;

    int currentWeapon;

    //  Vector3 weaponPosition;

    private int countAvailable;

    bool canChange;

    // Transform inventoryPanel;
    public Image pistolImage;
    public Image bulletImage;
    public TextMeshProUGUI bulletText;
    public TextMeshProUGUI xBullet;

    public Image batImage;
   
    //  public Sprite pistolSprite;
    // public Sprite batSprite;

    public Button pistolButton;
    public Button batButton;

    void Start()
    {
        weapons = new List<GameObject>();
        weaponAvailable = new List<bool>();

        pistolButton = GameObject.Find("UIPistola").GetComponent<Button>();
        pistolImage = GameObject.Find("UIPistola").GetComponent<Image>();
        bulletImage = GameObject.Find("ImageProiettili").GetComponent<Image>();
        bulletText = GameObject.Find("ProiettiliNumero").GetComponent<TextMeshProUGUI>();
        xBullet = GameObject.Find("xProiettili").GetComponent<TextMeshProUGUI>();

        batButton = GameObject.Find("UIMazza").GetComponent<Button>();
        batImage = GameObject.Find("UIMazza").GetComponent<Image>();


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
        pistolImage.enabled = false;
        bulletImage.enabled = false;
        xBullet.enabled = false;
        bulletText.enabled = false;

        //pistolImage.sprite = pistolSprite;
        pistolButton.enabled = false;
        //  pistolButton.enabled = false;

        GameObject mazza = GameObject.FindGameObjectWithTag("Mazza");
        weapons.Add(mazza);
        weaponAvailable.Add(false);
        weapons[2].SetActive(false); //il personaggio ha in mano la pistola all'inizio
        batImage.enabled = false;
        // batImage.sprite = batSprite;
        batButton.enabled = false;
        // batButton.SetActive(false);

        canChange = false;
        currentWeapon = 0;

        pistolButton.onClick.AddListener(TaskOnClickPistol);
        batButton.onClick.AddListener(TaskOnClickBat);
    }

    public UnityEvent<WeaponInventory> OnWeaponCollected;
    public void WeaponCollected(GameObject go)

    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].CompareTag(go.name))
                weaponAvailable[i] = true;

            if (go.name == "Pistola")
            {
                //  pistolImage.enabled = true;
                pistolButton.enabled = true;
                pistolImage.enabled = true;
                bulletImage.enabled = true;
                xBullet.enabled = true;
                bulletText.enabled = true;
                Debug.Log("Pistol collected");
            }

            if (go.name == "Mazza")
            {
                //  batImage.enabled = true;

                batButton.enabled = true;
                batImage.enabled = true;
                Debug.Log("Bat collected");
            }

        }

        OnWeaponCollected.Invoke(this);

    }

    void TaskOnClickPistol()
    {
        Debug.Log("You have clicked the Pistolbutton!");

        if (weapons[1].activeInHierarchy)
             setWeaponActive(0);
        else
            setWeaponActive(1);

    }

    void TaskOnClickBat()
    {
        Debug.Log("You have clicked the Batbutton!");

        if (weapons[2].activeInHierarchy)
        { setWeaponActive(0); }
        else
            setWeaponActive(2);
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