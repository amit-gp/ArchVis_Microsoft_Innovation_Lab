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
	#endregion

    public void Move(GameObject player, Camera camera, Vector3 movement, Quaternion localRotation, Quaternion yaw) {
        
        player.transform.Translate(movement);        
        player.transform.rotation = yaw;
        camera.transform.rotation = localRotation;
    }

    void Start () {
        transform.Translate(new Vector3(0, eyeHeight));    //Move to player height
    }

	void Update () {
		
	}
}
