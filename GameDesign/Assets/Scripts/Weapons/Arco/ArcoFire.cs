using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcoFire : MonoBehaviour
{
    public GameObject arrowPrefabs;

    public Transform arrowSpawn;
    public float arrowSpeed = 30f;
    public Vector3 rotation;

    public float lifeTime = 3f;
    public PlayerInventory pi;
    public GameObject player;

    public float count = 0;

    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (MainManager.Instance.numberOfArrows > 0)
        {
            Vector3 arrival = new Vector3(arrowSpawn.transform.position.x + 5, 0, 0);
            Vector3 Vo = CalculateVelocity(arrival, arrowSpawn.transform.position, 1f);
            transform.rotation = Quaternion.LookRotation(Vo);
            count++;

            Rigidbody arrowRb = arrowPrefabs.GetComponent<Rigidbody>();
            Rigidbody obj = Instantiate(arrowRb, arrowSpawn.position, Quaternion.identity);
            obj.velocity = Vo;

            Debug.Log("SPORE");

            StartCoroutine(DestroyArrowAfterTime(obj.gameObject, this.lifeTime));


        }
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

    private IEnumerator DestroyArrowAfterTime(GameObject arrow, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(arrow);
    }
}
