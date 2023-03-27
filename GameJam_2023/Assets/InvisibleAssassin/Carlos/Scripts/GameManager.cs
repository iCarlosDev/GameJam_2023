using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("--- PLAYER TRANSFORM ---")]
    [Space(10)]
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private Quaternion playerRotation;

    [Header("--- HEALTH ---")] 
    [Space(10)] 
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private int currentLives;
    
    [Header("--- HABILITY ---")] 
    [Space(10)] 
    [SerializeField] private int currentHability;

    [Header("--- CALIZ SOULS ---")] 
    [Space(10)] 
    [SerializeField] private int soulsRequired;

    [Header("--- ENEMY HEALTH ---")] 
    [Space(10)] 
    [SerializeField] private int enemyMaxHealth;
    
    [Header("--- WAVE PARAMETERS ---")] 
    [Space(10)] 
    [SerializeField] private int currentWave;

    //GETTERS && SETTERS//
    public Vector3 PlayerPosition
    {
        get => playerPosition;
        set => playerPosition = value;
    }
    public Quaternion PlayerRotation
    {
        get => playerRotation;
        set => playerRotation = value;
    }
    public GameObject HeartPrefab => heartPrefab;
    public int CurrentLives
    {
        get => currentLives;
        set => currentLives = value;
    }
    public int CurrentHability
    {
        get => currentHability;
        set => currentHability = value;
    }
    public int SoulsRequired
    {
        get => soulsRequired;
        set => soulsRequired = value;
    }
    public int EnemyMaxHealth
    {
        get => enemyMaxHealth;
        set => enemyMaxHealth = value;
    }
    public int CurrentWave
    {
        get => currentWave;
        set => currentWave = value;
    }

    /////////////////////////////////////
    
    private void Awake()
    {
        GameManagerDontDestroyOnLoad();
    }

    private void GameManagerDontDestroyOnLoad()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
