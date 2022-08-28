using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class DwarfController : MonoBehaviour
{
    public GameObject flipModel;

    public float detectionTime;

    bool firstDetection;
    public bool Attack;

    /* Variabili per Audio
    
    public AudioClip[] idleSounds;
    public float idleSoundTime;
    AudioSource enemyMovementAS;
    float nextIdleSound = 0f;
    */

    public float moveSpeed;
    public bool facingLeft = true;

    public Rigidbody myRB;
    public Animator myAnim;
    public Transform detectedPlayer;
    public GameObject player;

    public bool Detected;
    public bool dhLDeteced;



    public GameObject dwarf;
    public GameObject gnomo;
    public GameObject damage;
    public GameObject canvas;
    public GameObject detection;
    public DwarfHealthLisa dhL;

    void Start()
    {
        myRB = GetComponentInParent<Rigidbody>();
        myAnim = GameObject.Find("Gnomo").GetComponent<Animator>();

        // enemyMovementAS = GetComponent<AudioSource>

        Detected = false;
        Attack = false;
        firstDetection = false;
        if (Random.Range(0, 10) > 5) Flip();


        dwarf = GameObject.Find("Dwarf");
        canvas = GameObject.Find("dwarfFloatingHUD");
        detection = GameObject.Find("DwarfDetection");
        gnomo = GameObject.Find("Gnomo");
        damage = GameObject.Find("DwarfDamage");
        dhL = GameObject.Find("DwarfDamage").GetComponent<DwarfHealthLisa>();
        // player = GameObject.Find("ProtagonistaModel");
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = player.transform.position;
        pos.y = dwarf.transform.position.y;
        pos.z = dwarf.transform.position.z;


        damage.transform.position = new Vector3(gnomo.transform.position.x, 1.25f, gnomo.transform.position.z);
        detection.transform.position = new Vector3(gnomo.transform.position.x, 4f, gnomo.transform.position.z);
        canvas.transform.position = new Vector3(gnomo.transform.position.x, 3.5f, gnomo.transform.position.z);
        dhLDeteced = dhL.detected;

        if (Detected)
        {

            if (detectedPlayer.position.x > transform.position.x && facingLeft)
            {
                Flip();
            }
            else if (detectedPlayer.position.x < transform.position.x && !facingLeft)
            {
                Flip();
            }

            if (!firstDetection)
            {
                firstDetection = true;
            }
        }
        if (Detected)
        {
            if (!facingLeft)
            {

                if (dhL.detected)
                {

                    Attack = true;
                    myAnim.SetBool("attack", true);

                }
                else
                {
                   // myAnim.Play("Walking");
                    Attack = false;
                    myAnim.SetBool("attack", false);
                    myAnim.SetBool("walking", true);
                    gnomo.transform.position = Vector3.MoveTowards(gnomo.transform.position, pos, moveSpeed * Time.deltaTime);
                }
            }


            else
            {
                if (dhL.detected)
                {
                    Attack = true;
                    myAnim.SetBool("attack", true);
                }
                else
                {
                    Attack = false;
                    myAnim.SetBool("attack", false);
                    myAnim.SetBool("walking", true);
                    gnomo.transform.position = Vector3.MoveTowards(gnomo.transform.position, pos, moveSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            Attack = false;
            myAnim.SetBool("walking", false);
            gnomo.transform.position = gnomo.transform.position;
            myAnim.Play("Idle");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Detected)
        {
            player = other.gameObject;

            Detected = true;
            detectedPlayer = other.transform;
            myAnim.SetBool("detected", Detected);


            if (detectedPlayer.position.x < transform.position.x && facingLeft) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingLeft) Flip();

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            firstDetection = false;
            Detected = false;
        }
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = flipModel.transform.localScale;
        theScale.z *= -1;
        flipModel.transform.localScale = theScale;
    }
}