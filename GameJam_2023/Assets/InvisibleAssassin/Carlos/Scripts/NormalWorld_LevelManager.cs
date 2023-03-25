using System;
using UnityEngine;

public class NormalWorld_LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerStorage playerStorage;

    private void Awake()
    {
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
        playerStorage.PlayerController.Speed = 2f;
        playerStorage.PlayerController.TurnSmoothTime = 0.05f;
        playerStorage.PlayerController.Animator.SetFloat("PlayerSpeed", playerStorage.PlayerController.Speed);
        playerStorage.PlayerController.Animator.SetLayerWeight(1, 0);
        playerStorage.Sword.SetActive(false);
        playerStorage.Trail.SetActive(false);
    }
}
