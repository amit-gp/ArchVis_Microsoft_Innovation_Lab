/*
*	Copyright (C) Amit Kumar Gupta
*	Created by Amit Kumar Gupta
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour { 

    #region Public Variables
    public float height = 1.7f; //Player eye height in meters    
    public float walkSpeed = 0.5f;
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;
    #endregion

    #region Private Variables
    private ArchVisPlayerBehaviour playerBehaviour;
    private Camera playerCamera;
    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis
    #endregion

    void Start() {
        
        playerCamera = GetComponentInChildren<Camera>();
        playerBehaviour = gameObject.AddComponent<FPSBehaviour>() as FPSBehaviour; //Setting the default behaviour as FPS mode.
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

    }

    void Update() {

        Vector3 moveDirection = Vector3.zero;

        /*
        // Input based movement
        if (Input.GetKey(KeyCode.W))
            movementVector.z = 1;            
        if (Input.GetKey(KeyCode.A))
            movementVector.x = -1;
        if (Input.GetKey(KeyCode.S))
            movementVector.z = -1;
        if (Input.GetKey(KeyCode.D))
            movementVector.x = 1;
            */
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection *= walkSpeed * Time.deltaTime;
        //Debug.Log(moveDirection);

        //Mouse Look
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);      
        Quaternion yaw = Quaternion.Euler(0.0f, rotY, 0.0f);

        playerBehaviour.Move(this.gameObject, playerCamera, moveDirection, localRotation, yaw);

    }
}
