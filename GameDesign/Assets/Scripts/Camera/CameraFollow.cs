using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothing = 5f;
    public GameObject pl;

    Vector3 offset;

    public static bool exists;

    // Start is called before the first frame update
    void Awake()
    {
        pl = GameObject.FindGameObjectWithTag("Player");
        target = pl.transform;
        offset = transform.position - target.position;

    }
    void Start()
    {
        if (!exists)
        {
            exists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
