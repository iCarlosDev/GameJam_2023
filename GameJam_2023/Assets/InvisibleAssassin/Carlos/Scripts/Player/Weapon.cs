using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerStorage playerStorage;
    [SerializeField] private CinemachineImpulseSource _cinemachineImpulseSource;
    
    
    [Header("--- COMBO PARAMETERS ---")]
    [Space(10)]
    [SerializeField] private int comboNumber;
    [SerializeField] private float timeToFinishCombo;

    //GETTERS && SETTERS//
    public int ComboNumber
    {
        get => comboNumber;
        set => comboNumber = value;
    }
    public float TimeToFinishCombo => timeToFinishCombo;

    //////////////////////////////

    private void TextPopUp(Collider other)
    {
        comboNumber++;
        Vector3 textSpawnPos = new Vector3(other.transform.position.x, other.transform.position.y + 1.5f, other.transform.position.z);
        DynamicTextManager.CreateText(textSpawnPos, $"X{comboNumber}", DynamicTextManager.defaultData);

        playerStorage.PlayerController.FinishCombo();
    }

    private void TrailGrow()
    {
        if (playerStorage.Trail.time < 5)
        {
            playerStorage.Trail.time += 0.1f;
        }
    }

    private void AddHability()
    {
        if (GameManager.instance.CurrentHability < 10)
        {
            GameManager.instance.CurrentHability++;
            playerStorage.HealthBarController.SetHabilityParameters();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyColider"))
        {
            playerStorage.PlayerController.DashCombo = true;
            
            TextPopUp(other);
            TrailGrow();
            
            AddHability();

            if (playerStorage.PlayerController.Speed < 7.5f)
            {
                playerStorage.PlayerController.Speed += 0.3125f;
            }

            CameraShakeManager.instance.CameraShake(_cinemachineImpulseSource);
            
            AudioManager.instance.PlayOneShot("BoneBreak");
            
            playerStorage.PlayerController.StopDashing();

            if (playerStorage.PlayerController.DashCooldown != null)
            {
                playerStorage.PlayerController.StopCoroutine(playerStorage.PlayerController.DashCooldown);
                playerStorage.PlayerController.DashCooldown = null;
            }
            
            other.SendMessage("TakeDamage", 100);
        }
    }
}
