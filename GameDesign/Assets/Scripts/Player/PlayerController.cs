using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Movement Variables
    public float runSpeed;
    public Rigidbody myRB;
    public Animator myAnim;
    bool facingRight;

    // Jumping Variables
    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    //Sound
    private SoundManager soundManager;

    private static bool exists;  //da usare per i portali

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        facingRight = true;

        if (!exists)
        {
            exists = true;
           // DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }   //aggiunto per il passaggio tra i mondi  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (grounded && Input.GetButton("Jump"))
        {
            grounded = false;
            myAnim.SetBool("grounded", grounded);
            myRB.AddForce(new Vector3(0, jumpHeight, 0));
	    soundManager = FindObjectOfType<SoundManager>();
	    soundManager.SeleccionAudio(3,0.5f);
	    
        }

		
	   
	
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0) {
		grounded = true;
	}
        else grounded = false;

        myAnim.SetBool("grounded", grounded);

        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move));

        myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);

        /*
        //AGGIUNTA PER ANIMAZIONI UPPER BODY (??)
        if (MainManager.Instance.animationLayer == 2)
        {
            myAnim.SetBool("WithBat", true);
            myAnim.SetBool("WithGun", false);
            myAnim.SetBool("WithBow", false);
        }
        else if (MainManager.Instance.animationLayer == 3)
        {
            myAnim.SetBool("WithBat", false);
            myAnim.SetBool("WithGun", true);
            myAnim.SetBool("WithBow", false);
        }
        else if (MainManager.Instance.animationLayer == 4)
        {
            myAnim.SetBool("WithBat", false);
            myAnim.SetBool("WithGun", false);
            myAnim.SetBool("WithBow", true);
        }
        */

        if (move > 0 && !facingRight)
        {
            Flip();


        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
	    soundManager = FindObjectOfType<SoundManager>();
	    soundManager.SeleccionAudio(2,0.2f);
    }
}
