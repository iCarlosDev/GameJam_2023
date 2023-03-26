using System;
using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerStorage playerStorage;
    [SerializeField] private int comboNumber;

    [SerializeField] private CinemachineImpulseSource _cinemachineImpulseSource;

    //GETTERS && SETTERS//
    public int ComboNumber
    {
        get => comboNumber;
        set => comboNumber = value;
    }

    //////////////////////////////

    private void TextPopUp(Collider other)
    {
        comboNumber++;
        Vector3 textSpawnPos = new Vector3(other.transform.position.x, other.transform.position.y + 1.5f, other.transform.position.z);
        DynamicTextManager.CreateText(textSpawnPos, $"X{comboNumber}", DynamicTextManager.defaultData);
    }

    private void TrailGrow()
    {
        if (playerStorage.Trail.time < 5)
        {
            playerStorage.Trail.time += 0.1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyColider"))
        {
            playerStorage.PlayerController.DashCombo = true;
            
            TextPopUp(other);
            TrailGrow();
            
            CameraShakeManager.instance.CameraShake(_cinemachineImpulseSource);
            
            AudioManager.instance.PlayOneShot("BoneBreak");

            if (playerStorage.PlayerController.DashCooldown != null)
            {
                playerStorage.PlayerController.StopCoroutine(playerStorage.PlayerController.DashCooldown);
                playerStorage.PlayerController.DashCooldown = null;
            }
            
            other.SendMessage("TakeDamage", 100);
        }
    }
}
