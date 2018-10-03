using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControl : MonoBehaviour
{
    public float zoomSpeed = 2;
    public float zoomMin = -2f;
    public float zoomMax = -10f;

    public Transform target;

    private float zoom = -3;
    private float mouseX = 0;
    private float mouseY = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        turnToTarget();
        moveToTarget();
    }

    private void Update()
    {
        fixZoom();
        getInputForPosition();
    }

    // LateUpdate so that the target's position is defenetly set
    private void LateUpdate()
    {
        turnToTarget();
        moveToTarget();
    }

    private void fixZoom()
    {
        // Determin zoom / distance from target
        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        if (zoom > zoomMin)
        {
            zoom = zoomMin;
        }
        else if (zoom < zoomMax)
        {
            zoom = zoomMax;
        }
    }

    private void getInputForPosition()
    {
        // position camera
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");

        // Because the camera is a child to the targetpoint the target point is rotated, which in turn moves the camera
        mouseY = Mathf.Clamp(mouseY, -15f, 60f);
    }

    private void moveToTarget()
    {
        Vector3 destination = this.transform.forward * zoom;
        destination += target.position;
        this.transform.position = destination;
    }

    private void turnToTarget()
    {
        this.transform.eulerAngles = new Vector3(mouseY, mouseX); // set rotatoin
    }
}