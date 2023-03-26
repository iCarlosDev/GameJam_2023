using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    
    private void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _playerController.enabled = false;
        _playerController.Animator.SetTrigger("Death");
        StartCoroutine(GoNormalDimension_Coroutine());
    }

    private IEnumerator GoNormalDimension_Coroutine()
    {
        yield return new WaitForSeconds(5f);
        VirtualCameraController.instance.VirtualCameraBeginAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttackColider"))
        {
            if (currentHealth > 0)
            {
                TakeDamage(50);
            }
        }
    }
}
