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
    public static float walkSpeed = 5f;
    public static float mouseSensitivity = 100.0f;
    public static float clampAngle = 80.0f;
    #endregion

    #region Private Variables
    private ArchVisPlayerBehaviour playerBehaviour;
    public static Camera playerCamera;
    
    #endregion

    void Start() {
        
        playerCamera = GetComponentInChildren<Camera>();
        playerBehaviour = gameObject.AddComponent<FPSBehaviour>() as FPSBehaviour; //Setting the default behaviour as FPS mode.        
    }

    void Update() {
             
        playerBehaviour.Move(this.gameObject);

        if (Input.GetKeyDown(KeyCode.Space)) {
            
            if(playerBehaviour.GetType() == typeof(FPSBehaviour)) {
                Destroy(GetComponent("FPSBehaviour"));
                playerBehaviour = gameObject.AddComponent<EditModeBehaviour>() as EditModeBehaviour;
            }
                
            else if(playerBehaviour.GetType() == typeof(EditModeBehaviour)) {
                Destroy(GetComponent("EditModeBehaviour"));
                playerBehaviour = gameObject.AddComponent<FPSBehaviour>() as FPSBehaviour;
            }                

        }

    }
}
