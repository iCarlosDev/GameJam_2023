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
        
        AudioManager.instance.Play("NormalDimensionBackground");

        if (GameManager.instance.CurrentLives == 0)
        {
            GameManager.instance.CurrentLives = 5;
            GameManager.instance.CurrentHability = 0;
            
            playerStorage.HealthBarController.SetHealthBarParameters();
            playerStorage.HealthBarController.SetHabilityParameters();
        }
        
        FindObjectOfType<CalizSoulsPopUp>().gameObject.SetActive(false);
    }

    private void ScenePlayerParameters()
    {
        playerStorage.PlayerController.Speed = 2f;
        playerStorage.PlayerController.CurrentSpeed = 2f;
        playerStorage.PlayerController.TurnSmoothTime = 0.1f;
        playerStorage.PlayerController.Animator.SetFloat("PlayerSpeed", playerStorage.PlayerController.Speed);
        playerStorage.PlayerController.Animator.SetLayerWeight(1, 0);
        playerStorage.Sword.SetActive(false);
        playerStorage.Trail.gameObject.SetActive(false);
    }
}
