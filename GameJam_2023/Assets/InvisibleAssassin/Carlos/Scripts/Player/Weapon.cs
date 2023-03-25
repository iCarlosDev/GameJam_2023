using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyColider"))
        {
            _playerController.DashCombo = true;

            if (_playerController.DashCooldown != null)
            {
                _playerController.StopCoroutine(_playerController.DashCooldown);
                _playerController.DashCooldown = null;
            }
        }
    }
}
