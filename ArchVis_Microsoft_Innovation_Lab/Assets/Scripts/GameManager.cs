/*
*	Copyright (C) Amit Kumar Gupta
*	Created by Amit Kumar Gupta
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static Camera[] cameras;
    private static Player player;
    private static GameManager gameManagerInstance = new GameManager();
    private static readonly Object key = new Object();
    #endregion

    private GameManager() { }

    public static GameManager getInstance() {

        if (gameManagerInstance == null) {
            lock (key) {
                if (gameManagerInstance == null) {
                    gameManagerInstance = new GameManager();
                }
            }            
        }
        return gameManagerInstance;
    }

    private void Start() {

        cameras = Camera.allCameras;

    }

    private void Update() {
        

    }
}
