/*
*	Copyright (C) Amit Kumar Gupta
*	Created by Amit Kumar Gupta
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditModeBehaviour : MonoBehaviour, ArchVisPlayerBehaviour {
    
    #region Variables
    private float eyeHeight = 7.0f;
    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis
    private float mouseSensitivity = Player.mouseSensitivity;
    private float clampAngle = Player.clampAngle;
    private float clapmAngleTop = 0.0f;
    private Camera edCamera;
    private Texture2D crosshairImage;
    private Texture2D selectedCrosshairImage;
    private Texture2D currentCrosshair;
    private float walkSpeed = 9f;
    private Dictionary<GameObject, Material> highLightedObjects;
    #endregion

    public void Move(GameObject player) {

        Vector3 moveDirection = Vector3.zero;
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection *= walkSpeed * Time.deltaTime;

        //Mouse Look
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, clapmAngleTop, clampAngle);
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        Quaternion yaw = Quaternion.Euler(0.0f, rotY, 0.0f);

        player.transform.Translate(moveDirection);
        player.transform.rotation = yaw;
        edCamera.transform.rotation = localRotation;
    }

    void Start() {
        highLightedObjects = new Dictionary<GameObject, Material>();
        crosshairImage = GameManager.getInstance().editModeCrosshair;
        selectedCrosshairImage = GameManager.getInstance().editModeSelectedCrosshair;
        currentCrosshair = crosshairImage;
        edCamera = Player.playerCamera;           
        transform.position = new Vector3(transform.position.x, eyeHeight, transform.position.z);    //Move to player height
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void Update() {
        RaycastHit rayCastHit = new RaycastHit();
        if (Physics.Raycast(new Ray(edCamera.transform.position, edCamera.transform.forward), out rayCastHit)) {

            if (rayCastHit.transform.gameObject.tag != "Floor") {
                changeCrossHair(selectedCrosshairImage);
                highLightObject(rayCastHit.transform.gameObject);
                
            } else {
                if(currentCrosshair != crosshairImage && highLightedObjects.Count != 0) {
                    changeCrossHair(crosshairImage);
                    unhighLightAllObjects();
                }                
            }
        }
    }

    void OnGUI() {
        float xMin = (Screen.width / 2) - (currentCrosshair.width / 2);
        float yMin = (Screen.height / 2) - (currentCrosshair.height / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, currentCrosshair.width, currentCrosshair.height), currentCrosshair);
    }

    private void changeCrossHair(Texture2D targetTexture) {
        currentCrosshair = targetTexture;       
    }

    private void highLightObject(GameObject gObject) {        
        if(!highLightedObjects.ContainsKey(gObject))
            highLightedObjects.Add(gObject, gObject.GetComponent<Renderer>().material);
        gObject.GetComponent<Renderer>().material = GameManager.getInstance().highLightedMaterial;
    }

    private void unhighLightAllObjects() {        
        List<GameObject> keys = new List<GameObject>(highLightedObjects.Keys);
        foreach (GameObject hObject in keys) {            
            hObject.GetComponent<Renderer>().material = highLightedObjects[hObject];
            highLightedObjects.Remove(hObject);
        }
    }
}
