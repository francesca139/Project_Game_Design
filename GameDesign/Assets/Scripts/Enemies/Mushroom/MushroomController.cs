using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public GameObject flipModel;

    public float detectionTime;
    bool firstDetection;

    public float runSpeed;
    public bool facingLeft = true;

    Rigidbody myRB;
    Animator myAnim;
    Transform detectedPlayer;

    bool Detected;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponentInParent<Rigidbody>();
        myAnim = GetComponentInParent<Animator>();

        Detected = false;
        firstDetection = false;

        if (Random.Range(0, 10) > 5) Flip();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Detected)
        {
            if (detectedPlayer.position.x < transform.position.x && facingLeft) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingLeft) Flip();

            if(!firstDetection)
            {
                firstDetection = true;
            }
        }

        if(Detected && !facingLeft)
        {
            myRB.velocity = new Vector3(runSpeed * -1, myRB.velocity.y, 0);
        }
        else if (Detected && facingLeft)
        {
            myRB.velocity = new Vector3(runSpeed, myRB.velocity.y, 0);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Detected)
        {
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
