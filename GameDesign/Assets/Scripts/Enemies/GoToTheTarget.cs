using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTheTarget : MonoBehaviour
{
    public GameObject Target;
    public bool SnapToTarget = false;

    public float RotationSpeed = 2f;
    public float MovementSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {

        if (Target == null)
        {
            SimpleTarget simpleTarget = GameObject.FindObjectOfType<SimpleTarget>();

            if (simpleTarget != null)
            {
                Target = simpleTarget.gameObject;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetDirection = Target.transform.position - transform.position;
        targetDirection.y = 0f;
        targetDirection.Normalize();

        float rotationStep = RotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection, transform.up);

        transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);

    }
}
