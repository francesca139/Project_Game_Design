using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class DwarfController : MonoBehaviour
{
    public GameObject flipModel;

    public float detectionTime;
   

    public float runSpeed = 0.5f;
    public bool facingLeft = true;

    Rigidbody myRB;
    public Animator myAnim;
    public GameObject player;
    public Transform detectedPlayer;
    public Slider slider;
   

    public GameObject dwarf;
    public GameObject capsule;

    public bool Detected;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponentInParent<Rigidbody>();


        dwarf = GameObject.Find("Gnomo");
        capsule = GameObject.Find("DwarfDamage");
        myAnim = GetComponentInParent<Animator>();

        Detected = false;

        if (Random.Range(0, 10) > 5) Flip();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
      /*  if (Detected)
        {
            if (detectedPlayer.position.x < transform.position.x && facingLeft) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingLeft) Flip();

            if (!firstDetection)
            {
                firstDetection = true;
            }
        } */

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
