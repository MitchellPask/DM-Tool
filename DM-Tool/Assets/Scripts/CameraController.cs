﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;

    public float movementSpeed = 0.5f;
    public float movementTime = 5f;
    public float normalSpeed = 0.5f;
    public float fastSpeed = 3f;
    public float rotationAmount = 1f;
    public Vector3 zoomAmount;

    //Camera positioning
    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    public float panningBorderThickness = 1f; //DISABLED PANNING BORDER DURING DEVELOPMENT
    public Vector2 panLimit = new Vector2(40f, 60f);
    public float scrollSpeed = 20f;

    public float minY = 5f;
    public float maxY = 75f;

    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition; //Local position so camera stays relative to rig
    }

    //Update is called once per frame
    void Update()
    {
        //Camera Restrictions (clamps must stay above input checks)
        newPosition.x = Mathf.Clamp(newPosition.x, -panLimit.x, panLimit.x);
        newZoom.y = Mathf.Clamp(newZoom.y, minY, maxY);
        newPosition.z = Mathf.Clamp(newPosition.z, -panLimit.y, panLimit.y);

        //Function calls checking for movement input
        MouseInput();
        MovementInput();
    }

    void MouseInput()
    {
        //Zoom with scroll wheel
        if(Input.mouseScrollDelta.y != 0)
        {
            //Zoom distance limiting check
            if (!(newZoom.y >= maxY && Input.mouseScrollDelta.y < 0 || newZoom.y <= minY && Input.mouseScrollDelta.y > 0))
            {
                newZoom += Input.mouseScrollDelta.y * zoomAmount;
            }
        }

        //Click and drag movement with left mouse button
        if (Input.GetMouseButtonDown(2))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(2))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }

        //Rotation with right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            rotateStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            rotateCurrentPosition = Input.mousePosition;
            Vector3 difference = rotateStartPosition - rotateCurrentPosition;
            rotateStartPosition = rotateCurrentPosition;
            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f)); //difference negated for natural rotation
        }
    }

    void MovementInput()
    {
        //Keyboard movement speed (left-shift sprint)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        //Keyboard camera directional movement
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) /*|| Input.mousePosition.y >= Screen.height - panningBorderThickness*/)
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) /*|| Input.mousePosition.y <= panningBorderThickness*/)
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) /*|| Input.mousePosition.x >= Screen.width - panningBorderThickness*/)
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) /*|| Input.mousePosition.x <= panningBorderThickness*/)
        {
            newPosition += (transform.right * -movementSpeed);
        }

        //Keyboard camera Rotate
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        //Keyboard camera zoom
        if (Input.GetKey(KeyCode.R))
        {
            if (!(newZoom.y <= minY))
            {
                newZoom += zoomAmount;
            }
        }
        if (Input.GetKey(KeyCode.F))
        {
            if (!(newZoom.y >= maxY))
            {
                newZoom -= zoomAmount;
            }
        }


        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
