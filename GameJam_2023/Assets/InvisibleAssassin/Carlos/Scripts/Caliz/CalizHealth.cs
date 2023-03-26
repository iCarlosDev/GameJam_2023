using System;
using System.Collections.Generic;
using UnityEngine;

public class CalizHealth : MonoBehaviour
{
    public static CalizHealth instance;
    
    [SerializeField] private CalizWarning _calizWarning;
    
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    [SerializeField] private List<GameObject> enemiesList;

    private void Awake()
    {
        instance = this;
        _calizWarning = FindObjectOfType<CalizWarning>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        
        _calizWarning.gameObject.SetActive(false);
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
                takeDamage(1);
            }
        }

        if (other.CompareTag("Enemy"))
        {
            //Recorremos todos los enemigos que entran en nuestro collider;
            foreach (GameObject enemy in enemiesList)
            {
                //Comprobamos que el enemigo que entre no sea el mismo que ya existe en la lista, para no duplicar enemigos;
                if (enemy.transform.name.Equals(other.transform.name))
                {
                    return;
                }
            }
            
            _calizWarning.gameObject.SetActive(true);
            enemiesList.Add(other.gameObject);
        }
    }

    public void CheckEnemiesInRange(GameObject Enemy)
    {
        enemiesList.Remove(Enemy);

        if (enemiesList.Count == 0)
        {
            _calizWarning.gameObject.SetActive(false);
        }
    }
}
