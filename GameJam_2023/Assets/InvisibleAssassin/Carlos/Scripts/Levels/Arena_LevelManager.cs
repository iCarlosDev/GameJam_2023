using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena_LevelManager : MonoBehaviour
{
    public static Arena_LevelManager instance;
    
    [SerializeField] private PlayerStorage playerStorage;
    
    [Header("--- ENEMIES ---")] 
    [Space(10)] 
    [SerializeField] private List<EnemyHealth> enemiesList;
    [SerializeField] private List<Respawn> enemiesSpawnList;

    //GETTERS && SETTERS//
    public PlayerStorage PlayerStorage => playerStorage;

    //////////////////////////////////
    
    private void Awake()
    {
        instance = this;
        playerStorage = FindObjectOfType<PlayerStorage>();
        enemiesSpawnList.AddRange(FindObjectsOfType<Respawn>());
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerStorage.transform.position = GameManager.instance.PlayerPosition;
        playerStorage.transform.rotation = GameManager.instance.PlayerRotation;

        FindObjectOfType<CalizController>().enabled = false;
        
        ScenePlayerParameters();
        VirtualCameraController.instance.VirtualCameraEndAnimation();
    }
    
    private void ScenePlayerParameters()
    {
        playerStorage.PlayerController.Speed = 5f;
        playerStorage.PlayerController.CurrentSpeed = 5f;
        playerStorage.PlayerController.TurnSmoothTime = 0.5f;
        playerStorage.PlayerController.Animator.SetFloat("PlayerSpeed", playerStorage.PlayerController.Speed);
        playerStorage.PlayerController.Animator.SetLayerWeight(1, 1);
        playerStorage.Sword.SetActive(true);
        playerStorage.Trail.gameObject.SetActive(true);
    }

    public void GetAllEnemiesInScene()
    {
        enemiesList.AddRange(FindObjectsOfType<EnemyHealth>());

        foreach (Respawn enemySpawn in enemiesSpawnList)
        {
            enemySpawn.enabled = false;
        }
        
        foreach (EnemyHealth enemy in enemiesList)
        {
            enemy.TakeDamage(enemy.Enemy.vida);
        }
    }
}
