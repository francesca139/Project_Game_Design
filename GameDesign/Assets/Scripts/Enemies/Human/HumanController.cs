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


    public GameObject human;
    public GameObject capsule;
    public GameObject mask;
    public HumanHealth hhL;

    void Start()
    {
        myRB = GetComponentInParent<Rigidbody>();
        myAnim = GetComponentInParent<Animator>();

        // enemyMovementAS = GetComponent<AudioSource>

        Detected = false;
        Attack = false;
        firstDetection = false;
        if (Random.Range(0, 10) > 5) Flip();


        human = GameObject.Find("Human");
        capsule = GameObject.Find("HumanDamage");
        mask = GameObject.Find("Maschera");
        hhL = GameObject.Find("Nemico").GetComponent<HumanHealth>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        hhLDeteced = hhL.detected;
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

                if (hhL.detected)
                {

                    Attack = true;
                  //  myAnim.SetBool("attack", true);

                    myRB.velocity = new Vector3(0, myRB.velocity.y, 0);
                    capsule.GetComponent<Rigidbody>().velocity = new Vector3(0, myRB.velocity.y, 0);


                }
                else
                {
                    Attack = false;
                  //  myAnim.SetBool("attack", false);
                    myRB.velocity = new Vector3(moveSpeed, myRB.velocity.y, 0);
                    capsule.GetComponent<Rigidbody>().velocity = new Vector3(moveSpeed, myRB.velocity.y, 0);
                }
            }


            else
            {
                if (hhL.detected)
                {
                    Attack = true;
                  //  myAnim.SetBool("attack", true);
                    myRB.velocity = new Vector3(0, myRB.velocity.y, 0);
                    capsule.GetComponent<Rigidbody>().velocity = new Vector3(0, myRB.velocity.y, 0);
                }
                else
                {
                    Attack = false;
                 //   myAnim.SetBool("attack", false);
                    myRB.velocity = new Vector3((moveSpeed * -1), myRB.velocity.y, 0);
                    capsule.GetComponent<Rigidbody>().velocity = new Vector3((moveSpeed * -1), myRB.velocity.y, 0);
                }
            }
        }
        else
        {
            Attack = false;
            human.GetComponent<Rigidbody>().velocity = Vector3.zero;
            capsule.GetComponent<Rigidbody>().velocity = Vector3.zero;
            // myAnim.enabled = false;
            // myAnim.StopPlayback();
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

    public void Saved()
    {
        //ggiungere salvataggio
        mask.SetActive(false);
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
