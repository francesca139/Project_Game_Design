using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    //CONTROLLI
    public string scene;
    public bool one = false;

    //sliders
    public float fullHealth = 10;
    public float currentHealth;
    public Slider healthSlider;
    public Gradient gradientHb;
    public Image fillHb;

    public float fullFlight = 10;
    public float currentFlight;
    public Slider flightSlider;

    //inventario armi
    public List<GameObject> weapons;
    public List<bool> weaponAvailable;
    public int currentWeapon;

    public int countAvailable;
    public bool canChange;

    public Image elixirImage;
    public TextMeshProUGUI elixirText;
    public TextMeshProUGUI xElixir;
    public Button elixirButton;

    public TextMeshProUGUI numberOfPeopleText;
    public int typeProiettile;

    public Image batImage;
    public Button batButton;

    public Image pistolImage;
    public Image bulletImage;
    public TextMeshProUGUI bulletText;
    public TextMeshProUGUI xBullet;
    public Button pistolButton;
    public Button bulletButton;
    public Image sottoBullet;

    public Image arcoImage;
    public Image arrowImage;
    public TextMeshProUGUI arrowText;
    public TextMeshProUGUI xArrow;
    public Button arcoButton;
    public GameObject faretra;

    public bool wingsActive;
    public GameObject wings;

    public GameObject player;
    public WeaponInventory wi;

    //inventario
    public int numberOfPeople;
    public int numberOfArrows;
    public int numberOfElixirs;
    public int numberOfBullets;

    //danni
    public float bulletDamage;
    public float arrowDamage;
    public float elixirEffect;
    public float batDamage;

    public float animationLayer;

    public List<string> collected;

    //dialogo
    public GameObject popUpBox;
    public bool popUpActive;
    public Image popUpBoxImage;
    public TextMeshProUGUI popUpText;
    public Button popUpButton;
    public Image popUpButtonImage;


    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //___________________________________________________________________________________________________________
        //AGGIUNGERE TUTTE LE ARMI SULLA MANO DEL PERSONAGGIO ALL'INIZIO E TAGGARLE
        //le armi che si trovano nella scena invece non vanno taggate ma basta il nome per il confronto
        //Ogni arma deve essere aggiunta nella gerarchia singolarmente e non come ITEM in una folder
        //Ogni elemento che viene raccolto deve avere il tag STACKABLE (a parte le armi sulla mano)
        //___________________________________________________________________________________________________________

        GameObject empty = GameObject.FindGameObjectWithTag("EmptyHand");
        weapons.Add(empty);
        weaponAvailable.Add(true);
        weapons[0].SetActive(true);

        GameObject mazza = GameObject.FindGameObjectWithTag("Mazza");
        weapons.Add(mazza);
        weaponAvailable.Add(false);
        weapons[1].SetActive(false);

        GameObject pistola = GameObject.FindGameObjectWithTag("Pistola");
        weapons.Add(pistola);
        weaponAvailable.Add(false);
        weapons[2].SetActive(false);

        GameObject arco = GameObject.FindGameObjectWithTag("Arco");
        weapons.Add(arco);
        weaponAvailable.Add(false);
        weapons[3].SetActive(false);
        faretra = GameObject.FindGameObjectWithTag("Faretra");

        wings = GameObject.FindGameObjectWithTag("Wings");
       // wings.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        wi = player.GetComponent<WeaponInventory>();
        
        currentHealth = fullHealth;
        currentFlight = fullFlight;

        numberOfElixirs = 0;
        numberOfPeople = 0;
        numberOfArrows = 0;
        numberOfBullets = 10;

        bulletDamage = 1f;
        arrowDamage = 1f;
        elixirEffect = 1f;
        batDamage = 1f;

        typeProiettile = 0;

        animationLayer = 1;

        
    }

    private void FixedUpdate()
    {
        scene = SceneManager.GetActiveScene().name;


        //PER ELIMINARE GLI OGGETTI COLLEZIONATI
        for (int i = 0; i < collected.Count; i++)
        {
            GameObject temp = null;
            temp = GameObject.Find(collected[i]);

            if (temp != null)
            {
               // if (temp.tag == "Stackable")
                {
                    temp.SetActive(false);
                }
            }
        }


        //aggiornamento sliders
        fillHb = GameObject.FindGameObjectWithTag("FillHb").GetComponent<Image>();
        healthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        healthSlider.maxValue = fullHealth;
        healthSlider.value = currentHealth;
        fillHb.color = gradientHb.Evaluate(healthSlider.normalizedValue);

        if (SceneManager.GetActiveScene().name == "Fairy")
        {
            flightSlider = GameObject.FindGameObjectWithTag("FlightSlider").GetComponent<Slider>();
            flightSlider.maxValue = fullFlight;
            flightSlider.value = currentFlight;
        }

        //inventario armi
        batButton = GameObject.Find("UIMazza").GetComponent<Button>();
        batImage = GameObject.Find("UIMazza").GetComponent<Image>();

        if (!weaponAvailable[1])
        {
            batImage.enabled = false;
            batButton.enabled = false;
        }


        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            this.wingsActive = false;

            pistolButton = GameObject.Find("UIPistola").GetComponent<Button>();
            pistolImage = GameObject.Find("UIPistola").GetComponent<Image>();
            bulletImage = GameObject.Find("ImageProiettili").GetComponent<Image>();
            bulletText = GameObject.Find("ProiettiliNumero").GetComponent<TextMeshProUGUI>();
            xBullet = GameObject.Find("xProiettili").GetComponent<TextMeshProUGUI>();
            bulletButton = GameObject.Find("InfoPistola").GetComponent<Button>();
            this.sottoBullet = GameObject.Find("InfoPistola").GetComponent<Image>();


 
            elixirButton = GameObject.Find("InfoElixirs").GetComponent<Button>(); //solo nel mondo1

            if (!weaponAvailable[2])
            {
                pistolImage.enabled = false;
                bulletImage.enabled = false;
                xBullet.enabled = false;
                bulletText.enabled = false;
                pistolButton.enabled = false;

                bulletButton.enabled = false;
            }
        }

        //   popUpBox = GameObject.Find("PopUpBox");
        popUpBoxImage = GameObject.Find("PopUpBox").GetComponent<Image>();
        popUpButton = GameObject.Find("PopUpButton").GetComponent<Button>();
        popUpButtonImage = GameObject.Find("PopUpButton").GetComponent<Image>();
        popUpText = GameObject.Find("PopUpText").GetComponent<TextMeshProUGUI>();

        if (!popUpActive)
        {
            popUpBoxImage.enabled = false;
            popUpText.enabled = false;
            popUpButton.enabled = false;
            popUpButtonImage.enabled = false;
        }

        elixirImage = GameObject.Find("ImageElixirs").GetComponent<Image>();
        elixirText = GameObject.Find("ElixirsNumero").GetComponent<TextMeshProUGUI>();
        xElixir = GameObject.Find("xElixirs").GetComponent<TextMeshProUGUI>();

        numberOfPeopleText = GameObject.Find("NumberOfPeople").GetComponent<TextMeshProUGUI>();


        if (SceneManager.GetActiveScene().name == "Fairy")
        {
            this.wingsActive = true;

            one = false;
            arcoButton = GameObject.Find("UIArco").GetComponent<Button>();
            arcoImage = GameObject.Find("UIArco").GetComponent<Image>();
            arrowImage = GameObject.Find("ImageFrecce").GetComponent<Image>();
            arrowText = GameObject.Find("FrecceNumero").GetComponent<TextMeshProUGUI>();
            xArrow = GameObject.Find("xFrecce").GetComponent<TextMeshProUGUI>();

            if (!weaponAvailable[3])
            {
                arcoImage.enabled = false;
                arrowImage.enabled = false;
                xArrow.enabled = false;
                arrowText.enabled = false;

                arcoButton.enabled = false;
            }

        }






    }



}
