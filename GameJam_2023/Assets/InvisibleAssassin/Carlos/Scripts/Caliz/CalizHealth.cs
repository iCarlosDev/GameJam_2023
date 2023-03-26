using System;
using UnityEngine;

public class CalizHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    private void takeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Arena_LevelManager.instance.PlayerStorage.PlayerHealth.Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttackColider"))
        {
            if (currentHealth > 0)
            {
                takeDamage(10);
            }
        }
    }
}
