using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{

    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPosition;

    
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform transformB;

    // Start is called before the first frame update
    void Start()
    {
        posA = childTransform.localPosition; 
        posB = transformB.localPosition;
        nextPosition = posB;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        childTransform.localPosition =  Vector3.MoveTowards(childTransform.localPosition, nextPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(childTransform.localPosition, nextPosition) <= 0.1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        nextPosition = nextPosition != posA ? posA : posB;

    }

}
