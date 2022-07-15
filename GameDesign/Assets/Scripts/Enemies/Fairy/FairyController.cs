using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FairyController : MonoBehaviour
{

    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPosition;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Transform Transform;
    [SerializeField]
    private Transform transformB;

    public GameObject flipModel;

    public Slider slider;

    bool firstDetection;

    private bool facingRight = false;

    Animator myAnim;
    Transform detectedPlayer;



    bool Detected;

    // Start is called before the first frame update
    void Start()
    {
        posA = Transform.localPosition;
        posB = transformB.localPosition;
        nextPosition = posB;

        myAnim = GetComponentInParent<Animator>();

        Detected = false;
        firstDetection = false;

        if (Random.Range(0, 10) > 5) Flip();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Detected)
        {
            if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();

            if (!firstDetection)
            { 
                firstDetection = true;
            }
        }

        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !Detected)
        {
            Detected = true;
            detectedPlayer = other.transform;
            myAnim.SetBool("detected", Detected);

            if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            firstDetection = false;
        }
    }

    private void Move()
    {
        Transform.localPosition = Vector3.MoveTowards(Transform.localPosition, nextPosition, moveSpeed * Time.deltaTime);
        slider.transform.position = Vector3.MoveTowards(slider.transform.localPosition, nextPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(Transform.localPosition, nextPosition) <= 0.1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        nextPosition = nextPosition != posA ? posA : posB;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = flipModel.transform.localScale;
        theScale.z *= -1;
        flipModel.transform.localScale = theScale;
    }
}
