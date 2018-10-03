using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 80f;
    public float rotationSpeed = 100f;
    public bool lockMovement = false;
    public bool dodge = false;
    public float dodgeDashSpeed = 2000f;

    private Transform cameraTransform;
    private Rigidbody myRigidbody;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Physics should be handled in FixedUpdate
    private void FixedUpdate()
    {
        // Fix the rotation of the player every frame, unless the right mouse button is pressed
        if (!Input.GetMouseButton(1))
        {
            Quaternion turnAngle = Quaternion.AngleAxis(cameraTransform.transform.eulerAngles.y, Vector3.up);
            myRigidbody.rotation = Quaternion.Slerp(this.transform.rotation, turnAngle, Time.deltaTime * rotationSpeed);
        }

        float moveFB = Input.GetAxis("Vertical");
        float moveLR = Input.GetAxis("Horizontal");


        if (moveFB != 0 || moveLR != 0)
        {
            Vector3 movement = Vector3.zero;

            if (!lockMovement)
            {
                movement = (myRigidbody.transform.forward * moveFB + myRigidbody.transform.right * moveLR) * moveSpeed;

                myRigidbody.AddForce(movement);
            }
            else
            {
                myRigidbody.velocity = Vector3.zero;
            }

            if (dodge)
            {
                dash(moveFB, moveLR);
            }

            dodge = false;
        }
    }

    private void dash(float moveFB, float moveLR)
    {
        Vector3 dodgeVector = Vector3.zero;

        if (moveFB > 0)
        {
            dodgeVector += 1 * myRigidbody.transform.forward;
        }
        else if (moveFB < 0)
        {
            dodgeVector += -1 * myRigidbody.transform.forward;
        }

        if (moveLR > 0)
        {
            dodgeVector += 1 * myRigidbody.transform.right;
        }
        else if (moveLR < 0)
        {
            dodgeVector += -1 * myRigidbody.transform.right;
        }

        myRigidbody.AddForce(dodgeVector * dodgeDashSpeed);}
}