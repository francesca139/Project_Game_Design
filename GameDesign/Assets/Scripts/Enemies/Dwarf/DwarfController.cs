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

    public bool Detected;

    /* Non so cosa siano queste variabili 
    
    public GameObject player;
    public Slider slider;
    public GameObject dwarf;
    public GameObject capsule;

    */

    void Start()
    {
        myRB = GetComponentInParent<Rigidbody>();
        myAnim = GetComponentInParent<Animator>();

        // enemyMovementAS = GetComponent<AudioSource>

        Detected = false;
        Attack = false;
        firstDetection = false;
        if (Random.Range(0, 10) > 5) Flip();

        /* 
        dwarf = GameObject.Find("Gnomo");
        capsule = GameObject.Find("DwarfDamage");
        */

    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        if (Detected && !facingLeft) 
        {
            if (detectedPlayer.position.x - transform.position.x < 0.1)
            {
                Attack = true;
                myAnim.SetBool("attack", Attack);
                myRB.velocity = new Vector3(0.1, myRB.velocity.y, 0);
            }
            else
            {
                Attack = false;
                myAnim.SetBool("attack", Attack); 
                myRB.velocity = new Vector3(moveSpeed, myRB.velocity.y, 0);
            }
        }
       
        else if (Detected && facingLeft)
        {
            if (detectedPlayer.position.x - transform.position.x < 0.1)
            {
                Attack = true;
                myAnim.SetBool("attack", Attack);
                myRB.velocity = new Vector3(-1, myRB.velocity.y, 0);
            }
            else
            {
                Attack = false;
                myAnim.SetBool("attack", Attack);
                myRB.velocity = new Vector3((moveSpeed * -1), myRB.velocity.y, 0);
            }
        }

        /*
        if (Detected)
        {
            Vector3 pos = player.transform.position;
            pos.y = dwarf.transform.position.y;
            float distance = transform.position.x - player.transform.position.x;

            if (distance < 0 && facingLeft)
                Flip();
            if (distance > 0 && !facingLeft)
                Flip();

            dwarf.transform.position = Vector3.MoveTowards(dwarf.transform.position, pos, runSpeed * Time.deltaTime);
            capsule.transform.position = Vector3.MoveTowards(dwarf.transform.position, pos, runSpeed * Time.deltaTime);

            Vector3 posSlider = pos;
            posSlider.y = slider.transform.position.y;
            slider.transform.position = Vector3.MoveTowards(slider.transform.position, posSlider, runSpeed * Time.deltaTime);
            
        }
        else
        {
            capsule.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Detected)
        {
            // player = other.gameObject;

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
