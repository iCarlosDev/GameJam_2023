using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitParticle;

    [SerializeField] private Enemy _enemy;

    private void Awake()
    {
        _enemy = transform.parent.GetComponent<Enemy>();
    }

    private void Start()
    {
        hitParticle.Stop();
    }

    public void TakeDamage(int damage)
    {
        _enemy.vida -= damage;
        hitParticle.Play();

        if (_enemy.vida <= 0)
        {
            _enemy.Die();
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            if (_enemy.vida > 0)
            {
                TakeDamage(_enemy.vida);    
            }
        }
    }*/
}
