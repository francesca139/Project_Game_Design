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
    bool Attack;

    /* Variabili per Audio
    
    public AudioClip[] idleSounds;
    public float idleSoundTime;
    AudioSource enemyMovementAS;
    float nextIdleSound = 0f;
    */

    public float moveSpeed;
    public bool facingLeft = true;

    Rigidbody myRB;
    public Animator myAnim;
    public Transform detectedPlayer;

    public GameObject player;

    public bool Detected;
    public bool dhLDeteced;


    public GameObject dwarf;
    public GameObject capsule;
    public DwarfHealthLisa dhL;

    void Start()
    {
        myRB = GetComponentInParent<Rigidbody>();
        myAnim = GetComponentInParent<Animator>();

        // enemyMovementAS = GetComponent<AudioSource>

        Detected = false;
        Attack = false;
        firstDetection = false;
        if (Random.Range(0, 10) > 5) Flip();

        dwarf = GameObject.Find("Dwarf");
        capsule = GameObject.Find("DwarfDamage");
        dhL = GameObject.Find("Gnomo").GetComponent<DwarfHealthLisa>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

                    myRB.velocity = new Vector3(0, myRB.velocity.y, 0);
                    capsule.GetComponent<Rigidbody>().velocity = new Vector3(0, myRB.velocity.y, 0);


                }
                else
                {
                    Attack = false;
                    myAnim.SetBool("attack", false);
                    myRB.velocity = new Vector3(moveSpeed, myRB.velocity.y, 0);
                    capsule.GetComponent<Rigidbody>().velocity = new Vector3(moveSpeed, myRB.velocity.y, 0);
                }
            }


            else
            {
                if (dhL.detected)
                {
                    Attack = true;
                    myAnim.SetBool("attack", true);
                    myRB.velocity = new Vector3(0, myRB.velocity.y, 0);
                    capsule.GetComponent<Rigidbody>().velocity = new Vector3(0, myRB.velocity.y, 0);
                }
                else
                {
                    Attack = false;
                    myAnim.SetBool("attack", false);
                    myRB.velocity = new Vector3((moveSpeed * -1), myRB.velocity.y, 0);
                    capsule.GetComponent<Rigidbody>().velocity = new Vector3((moveSpeed * -1), myRB.velocity.y, 0);
                }
            }
        }
        else
        {
            Attack = false;
            dwarf.GetComponent<Rigidbody>().velocity = Vector3.zero;
            capsule.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
