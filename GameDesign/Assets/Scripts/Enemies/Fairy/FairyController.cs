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

    public GameObject canvasSlider;

    bool firstDetection;

    private bool facingRight = false;

    public Animator myAnim;
    Transform detectedPlayer;

    public GameObject sporePrefab;
    public Transform sporeSpawn;

    public float timeBetween;
    public float lifeTime;


    bool Detected;

    // Start is called before the first frame update
    void Start()
    {
        posA = Transform.localPosition;
        posB = transformB.localPosition;
        nextPosition = posB;


        this.timeBetween = 2f;
        this.lifeTime = 3f;

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

            StartCoroutine(LaunchSporeAfterTime(sporePrefab, timeBetween));
        

         }
    }

    private IEnumerator LaunchSporeAfterTime(GameObject spore, float delay)
    {
        while (Detected)
        {
            yield return new WaitForSeconds(delay);

            {
                Vector3 Vo = CalculateVelocity(detectedPlayer.transform.position, sporeSpawn.transform.position, 1f);
                // transform.rotation = Quaternion.LookRotation(Vo);

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
            Detected = false;
        }
    }

    private void Move()
    {
        Transform.localPosition = Vector3.MoveTowards(Transform.localPosition, nextPosition, moveSpeed * Time.deltaTime);
        canvasSlider.transform.position = Vector3.MoveTowards(canvasSlider.transform.localPosition, nextPosition, moveSpeed * Time.deltaTime);

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
