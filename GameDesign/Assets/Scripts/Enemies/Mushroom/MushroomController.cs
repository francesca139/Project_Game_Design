using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public GameObject flipModel;

    public float detectionTime;
    bool firstDetection;

    public float runSpeed = 0f;
    public bool facingLeft = true;

    Rigidbody myRB;
    Animator myAnim;
    Transform detectedPlayer;

    bool Detected;

    public GameObject sporePrefab;
    public Transform sporeSpawn;
    public GameObject player;

    public float timeBetween;
    public float lifeTime;

    public int count;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponentInParent<Rigidbody>();
        myAnim = GetComponentInParent<Animator>();

        Detected = false;
        firstDetection = false;

        if (Random.Range(0, 10) > 5) Flip();

        player = GameObject.FindGameObjectWithTag("Player");
        this.timeBetween = 1f;
        this.lifeTime = 3f;
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
            myRB.velocity = new Vector3(runSpeed * -1, 0, 0);
                //myRB.velocity.y, 0);
        }
        else if (Detected && facingLeft)
        {
            myRB.velocity = new Vector3(runSpeed, 0, 0);
                //myRB.velocity.y, 0);
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


            //per il lancio delle spore
            StartCoroutine(LaunchSporeAfterTime(sporePrefab, timeBetween));

        }
    }


     private IEnumerator LaunchSporeAfterTime(GameObject spore, float delay)
     {
        while (Detected)
        {
              yield return new WaitForSeconds(delay);

            {
                Vector3 Vo = CalculateVelocity(player.transform.position, sporeSpawn.transform.position, 1f);
                transform.rotation = Quaternion.LookRotation(Vo);
                count++;

                Rigidbody sporePrefabRb = sporePrefab.GetComponent<Rigidbody>();
                Rigidbody obj = Instantiate(sporePrefabRb, sporeSpawn.position, Quaternion.identity);
                obj.velocity = Vo;

                Debug.Log("SPORE");

                 StartCoroutine(DestroySporeAfterTime(obj.gameObject, this.lifeTime));

             }
        }
    }

    private IEnumerator DestroySporeAfterTime(GameObject spore, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(spore);
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //distanza
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;

        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;

        result = result * Vxz;
        result.y = Vy;

        return result;

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
