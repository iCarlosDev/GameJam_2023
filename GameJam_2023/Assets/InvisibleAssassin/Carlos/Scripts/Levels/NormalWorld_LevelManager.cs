using System;
using UnityEngine;

public class NormalWorld_LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerStorage playerStorage;

    [SerializeField] private CalizController _calizController;

    private void Awake()
    {
        playerStorage = FindObjectOfType<PlayerStorage>();
        _calizController = FindObjectOfType<CalizController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        ScenePlayerParameters();
        VirtualCameraController.instance.VirtualCameraEndAnimation();
        
        AudioManager.instance.Play("NormalDimensionBackground");

        GameManager.instance.SoulsRequired += 20;
        GameManager.instance.CurrentWave++;
        
        if (GameManager.instance.CurrentLives <= 0)
        {
            GameManager.instance.CurrentLives = 5;
            GameManager.instance.CurrentHability = 0;
            GameManager.instance.SoulsRequired = 10;
            GameManager.instance.CurrentWave = 1;
            
            playerStorage.HealthBarController.SetHealthBarParameters();
            playerStorage.HealthBarController.SetHabilityParameters();
        }
        
        FindObjectOfType<CalizSoulsPopUp>().gameObject.SetActive(false);
        _calizController.transform.GetChild(3).gameObject.SetActive(false);
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
