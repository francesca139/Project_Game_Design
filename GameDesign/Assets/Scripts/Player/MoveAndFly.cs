using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//DA METTERE AL POSTO DELLO SCRIPT MOVEMENT sul protagonista
public class MoveAndFly : MonoBehaviour
{
    // Movement Variables
    public float runSpeed;
    Rigidbody myRB;
    Animator myAnim;
    bool facingRight;

    // Jumping Variables
    public bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    //Flight Variables
    public float fullTimeFly = 5000;
    public float timeInair = 0;

    public float fs;

    public float fullSlider;
    public float actualSlider;

    public Slider flightSlider;

    public bool pressed = false;
    public bool doublePressed = false;


    public float timePressed;


    public bool fly = false;
    public float actualTime;
    public float time;

    public Coroutine coD = null;
    public Coroutine coI = null;

    //Sound variables
    private SoundManager soundmanager;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        facingRight = true;

        this.fullSlider = MainManager.Instance.fullFlight;
        this.actualSlider = MainManager.Instance.currentFlight;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (SceneManager.GetActiveScene().name == "Fairy") 
            this.flightSlider = MainManager.Instance.flightSlider;

        time = Time.time;
        if (Input.GetButtonDown("Jump"))
        {
            actualTime = Time.time;
            //  count++;
        }
        if (Input.GetButton("Jump"))
        {
            if (grounded)
            {
                grounded = false;
                myAnim.SetBool("grounded", grounded);
                myRB.AddForce(new Vector3(0, jumpHeight, 0));

                fly = false;
                pressed = true;
                timePressed = Time.time - actualTime;

            }
        }

       
            if (pressed && Time.time + timePressed > actualTime)
            {
                if (Input.GetButtonDown("Jump"))
                { doublePressed = true; }
            }

        if (SceneManager.GetActiveScene().name == "Fairy")
        {
            if (doublePressed)
            {
                timeInair = timeInair + Time.time;
                fly = true;
                if (this.actualSlider > 1)
                {
                    Vector3 inAir = new Vector3(transform.position.x, transform.position.y, 0);
                    transform.position = inAir;
                    Vector3 pos = new Vector3(transform.position.x, 3.5f, 0);
                    transform.position = Vector3.MoveTowards(transform.position, pos, runSpeed * Time.deltaTime);

                    float move1 = Input.GetAxis("Horizontal");

                    myRB.velocity = transform.forward * runSpeed;

                    if (Input.GetKey("down"))
                        doublePressed = false;
                }
            }
        }
        else
        { timeInair = 0; }




        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (groundCollisions.Length > 0)
        {
            grounded = true;
            fly = false;
            pressed = false;
            timePressed = 0;

            doublePressed = false;

            timeInair = 0;
		
		//suono
	    //var audioSource = GetComponent<AudioSource>();
	    //audioSource.Play();
	    soundmanager = FindObjectOfType<SoundManager>();
	    soundmanager.SeleccionAudio(1,0.5f);

        }
        else
            grounded = false;


        myAnim.SetBool("grounded", grounded);

        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move));

        myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);

        //per sparare
        float firing = Input.GetAxis("Fire1");
        //  myAnim.SetFloat("shooting", firing);

        //per sparare si ferma?
        // if ((myRB.velocity.magnitude > 0 && firing > 0) && grounded)
        //    myRB.velocity = new Vector3(move * 0f, myRB.velocity.y, 0);


        if (move > 0 && !facingRight) Flip();
        else if (move < 0 && facingRight) Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
	 soundmanager = FindObjectOfType<SoundManager>();
	soundmanager.SeleccionAudio(2,1f);

    }


    //per sparare
    public float GetFacing()
    {
        if (facingRight)
            return 1;
        else
            return -1;

    }

    public void DecreaseSlider()
    {
        this.actualSlider = this.actualSlider - 0.5f;

        flightSlider.value = MainManager.Instance.currentFlight;
        MainManager.Instance.currentFlight = actualSlider;
        fs = flightSlider.value;
    }

    public void IncreaseSlider()
    {
        this.actualSlider = this.actualSlider + 0.5f;

        flightSlider.value = MainManager.Instance.currentFlight;
        MainManager.Instance.currentFlight = actualSlider;
        fs = flightSlider.value;
    }
}
