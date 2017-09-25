/*
*	Copyright (C) Amit Kumar Gupta
*	Created by Amit Kumar Gupta
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSBehaviour : MonoBehaviour, ArchVisPlayerBehaviour
{
    #region Variables
    private float eyeHeight = 1.7f;
    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis
    private float mouseSensitivity = Player.mouseSensitivity;
    private float clampAngle = Player.clampAngle;
    private Camera fpsCamera;
    private float walkSpeed = Player.walkSpeed;
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
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        Quaternion yaw = Quaternion.Euler(0.0f, rotY, 0.0f);

        player.transform.Translate(moveDirection);        
        player.transform.rotation = yaw;
        fpsCamera.transform.rotation = localRotation;
    }

    void Start () {
        fpsCamera = Player.playerCamera;
        StartCoroutine(MoveToStartCoroutine(new Vector3(transform.position.x, eyeHeight, transform.position.z)));    //Move to player height
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    IEnumerator MoveToStartCoroutine(Vector3 targetPosition) {
        float timeToStart = Time.time;
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f) {
            transform.position = Vector3.Lerp(transform.position, targetPosition, (Time.time - timeToStart) * 1); //Here speed is the 1 or any number which decides the how fast it reach to one to other end.
            yield return null;
        }
        yield return new WaitForSeconds(3f); // THis is just for how Coroutine works with delay
    }

    void Update () {
		
	}
}
