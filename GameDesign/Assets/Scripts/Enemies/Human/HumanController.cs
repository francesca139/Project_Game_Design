using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
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
    public bool hhLDeteced;
    public bool free;


    public GameObject human;
    public GameObject capsule;
    public GameObject mask;
    public GameObject maglietta;
    public HumanHealth hhL;

    void Start()
    {
        myRB = GetComponentInParent<Rigidbody>();
        //   myAnim = GetComponentInParent<Animator>();
        myAnim = GameObject.Find("Nemico").GetComponent<Animator>();

        // enemyMovementAS = GetComponent<AudioSource>

        Detected = false;
        Attack = false;
        free = false;
        firstDetection = false;
        if (Random.Range(0, 10) > 5) Flip();


        human = GameObject.Find("Human");
        capsule = GameObject.Find("HumanDamage");
        mask = GameObject.Find("Maschera");
        maglietta = GameObject.Find("Maglietta");
        hhL = GameObject.Find("HumanDamage").GetComponent<HumanHealth>();

        hhLDeteced = hhL.detected;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        hhLDeteced = hhL.detected;
        capsule.transform.position = new Vector3(maglietta.transform.position.x, 2.5f, maglietta.transform.position.z);
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
        if (Detected && !free)
        {
            if (!facingLeft)
            {

                if (hhLDeteced)
                {

                    Attack = true;
                    myAnim.SetBool("attack", true);

                    myRB.velocity = new Vector3(0, 0, 0);
                   // capsule.GetComponent<Rigidbody>().velocity = new Vector3(0, myRB.velocity.y, 0);
                    mask.GetComponent<Rigidbody>().velocity = new Vector3(0, myRB.velocity.y, 0);


                }
                else
                {
                    Attack = false;
                    myAnim.SetBool("attack", false);
                    myRB.velocity = new Vector3(moveSpeed, myRB.velocity.y, 0);
                   // capsule.GetComponent<Rigidbody>().velocity = new Vector3(moveSpeed, myRB.velocity.y, 0);
                    mask.GetComponent<Rigidbody>().velocity = new Vector3(moveSpeed, myRB.velocity.y, 0);
                }
            }


            else
            {
                if (hhLDeteced)
                {
                    Attack = true;
                    myAnim.SetBool("attack", true);
                    myRB.velocity = new Vector3(0, 0, 0);
                   // capsule.GetComponent<Rigidbody>().velocity = new Vector3(0, myRB.velocity.y, 0);
                    mask.GetComponent<Rigidbody>().velocity = new Vector3(0, myRB.velocity.y, 0);
                }
                else
                {
                    Attack = false;
                    myAnim.SetBool("attack", false);
                    myRB.velocity = new Vector3((moveSpeed * -1), myRB.velocity.y, 0);
                   // capsule.GetComponent<Rigidbody>().velocity = new Vector3((moveSpeed * -1), myRB.velocity.y, 0);
                    mask.GetComponent<Rigidbody>().velocity = new Vector3((moveSpeed * -1), myRB.velocity.y, 0);
                }
            }
        }
        else
        {
            Attack = false;
            human.GetComponent<Rigidbody>().velocity = Vector3.zero;
           // capsule.GetComponent<Rigidbody>().velocity = Vector3.zero;
            // myAnim.enabled = false;
            // myAnim.StopPlayback();
            myAnim.Play("Idle1");
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

    public void Saved()
    {
        //aggiungere animazione salvataggio
        //  mask.SetActive(false);
        free = true; 

        myRB.velocity = new Vector3(0, myRB.velocity.y, 0);
        capsule.GetComponent<Rigidbody>().velocity = new Vector3(0, myRB.velocity.y, 0);

        mask.GetComponent<Rigidbody>().velocity = new Vector3(0, - 40f * Time.deltaTime, 0);

        Debug.Log("SavedDDDDDDDDDDDD");
    }

    public void damaged()
    {
        //aggiungere animazione del danno
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = flipModel.transform.localScale;
        theScale.z *= -1;
        flipModel.transform.localScale = theScale;
    }
}
