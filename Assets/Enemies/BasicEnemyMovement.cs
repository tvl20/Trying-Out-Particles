using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasicEnemyMovement : MonoBehaviour
{
    public float TargetDistanceFromPlayer = 5;
    public float moveSpeed = 3;
    public float rotationSpeed = 15f;
    public bool lockMovement = false;
    public Transform target = null;

    private Rigidbody myRigidbody;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Physics should be handled in FixedUpdate
    private void FixedUpdate()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(target.position, this.transform.position);
            Vector3 turnAngle = target.position - myRigidbody.position;
            myRigidbody.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(turnAngle),
                Time.deltaTime * rotationSpeed);

            if (!lockMovement)
            {
                if (distanceToTarget > TargetDistanceFromPlayer)
                {
                    myRigidbody.velocity = myRigidbody.transform.forward * moveSpeed;
                }
                else if (distanceToTarget < TargetDistanceFromPlayer - 0.5f
                ) // little buffer space to prevent yittering
                {
                    myRigidbody.velocity = myRigidbody.transform.forward * moveSpeed * -1;
                }
            }
            else
            {
                myRigidbody.velocity = Vector3.zero;
            }
        }
    }
}