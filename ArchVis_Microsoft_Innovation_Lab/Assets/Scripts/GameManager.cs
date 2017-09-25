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
    private static Player player;
    private static GameManager gameManagerInstance;

    [Header("GUI Elements")]
    public Texture2D editModeCrosshair;
    public Texture2D editModeSelectedCrosshair;
    public Material highLightedMaterial;
    #endregion

    private GameManager() { }

    private void Awake() {
        
        if(gameManagerInstance == null) {
            gameManagerInstance = this;
        } else if(gameManagerInstance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public static GameManager getInstance() {
        
        return gameManagerInstance;
    }

    private void Start() {

    }

    private void Update() {        

    }
}
