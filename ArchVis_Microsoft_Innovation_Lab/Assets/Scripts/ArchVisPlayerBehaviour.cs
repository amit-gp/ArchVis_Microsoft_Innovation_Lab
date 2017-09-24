/*
*	Copyright (C) Amit Kumar Gupta
*	Created by Amit Kumar Gupta
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ArchVisPlayerBehaviour {

    void Move(GameObject player, Camera camera, Vector3 movement, Quaternion lookRotation, Quaternion yaw);
}
