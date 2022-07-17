using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//DA METTERE SUL COLLIDER DEL MAIN CHARACTER
public class ColliderMovement : MonoBehaviour
{
    public GameObject player;
    public MoveAndFly move;

    public bool detected = false;

  //  public float fullTimeFly;
  //  public float timeInair;

    public float fs;
    public bool fly = false;

    // Jumping Variables
    public bool grounded;
    public float jumpHeight;

    //Flight Variables


   // public float fullSlider;
  //  public float actualSlider;

  //  public Slider flightSlider;

    public Coroutine coD = null;
    public Coroutine coI = null;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        move = player.GetComponent<MoveAndFly>();

        //this.fullSlider = move.fullSlider;
       // this.actualSlider = move.actualSlider;

       // this.fullTimeFly = move.fullTimeFly;

       // this.flightSlider = move.flightSlider;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (SceneManager.GetActiveScene().name == "Fairy")
        {
          //  this.fly = move.fly;
          //  this.timeInair = move.timeInair;


            if (!detected && coD != null)
            {
                StopCoroutine(coD);
                Debug.Log("Stopped decreasement");
            }

            if (detected && coI != null)
            {
                StopCoroutine(coI);
                Debug.Log("Stopped increasement");
            }

        }

        Vector3 position = new Vector3(player.transform.position.x, transform.position.y, 0);
        transform.position = position;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "Fairy")
        {
            if (other.tag == "Player")
            {
                Debug.Log("Collided");
                detected = true;
                coD = StartCoroutine(FlightSliderDecrease());
            }
        }


    }

    private IEnumerator FlightSliderDecrease()
    {
        var wait = new WaitForSeconds(1);
        Debug.Log("Flight slider decreased");
        while (detected)
        {
            Debug.Log("Flight slider decreased 2");
            yield return wait;
            move.DecreaseSlider();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "Fairy")
        {
            if (other.CompareTag("Player"))
            {
                detected = false;
                coI = StartCoroutine(FlightSliderIncrease());
                Debug.Log("Exited");
            }
        }
    }

    private IEnumerator FlightSliderIncrease()
    {
        var wait = new WaitForSeconds(1);
        Debug.Log("increase slider");

        while (!detected)
        {
            Debug.Log("Flight slider increased 2");
            yield return wait;
            move.IncreaseSlider();
        }

    }
}
