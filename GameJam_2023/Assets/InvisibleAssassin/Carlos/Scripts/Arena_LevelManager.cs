using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena_LevelManager : MonoBehaviour
{
    public static Arena_LevelManager instance;
    
    [SerializeField] private PlayerStorage playerStorage;

    //GETTERS && SETTERS//
    public PlayerStorage PlayerStorage => playerStorage;

    //////////////////////////////////
    
    private void Awake()
    {
        instance = this;
        playerStorage = FindObjectOfType<PlayerStorage>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        ScenePlayerParameters();
        VirtualCameraController.instance.VirtualCameraEndAnimation();
    }
    
    private void ScenePlayerParameters()
    {
        playerStorage.PlayerController.Speed = 5f;
        playerStorage.PlayerController.TurnSmoothTime = 0.1f;
        playerStorage.PlayerController.Animator.SetFloat("PlayerSpeed", playerStorage.PlayerController.Speed);
        playerStorage.PlayerController.Animator.SetLayerWeight(1, 1);
        playerStorage.Sword.SetActive(true);
        playerStorage.Trail.gameObject.SetActive(true);
    }
}
