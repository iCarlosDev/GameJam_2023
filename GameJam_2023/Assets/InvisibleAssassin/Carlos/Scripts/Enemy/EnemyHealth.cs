using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameObject enemySoul;
    [SerializeField] private Transform enemySoulDestination;
    [SerializeField] private bool soulGoCaliz;

    public Enemy Enemy => _enemy;

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
            GameObject soul = Instantiate(enemySoul, transform.position, Quaternion.identity);

            if (soulGoCaliz)
            {
                soul.GetComponent<EnemySoul>().Target = FindObjectOfType<CalizSouls>().transform;
            }
            else
            {
                soul.GetComponent<EnemySoul>().Target = enemySoulDestination;  
            }

            enemySoulDestination.parent = null;
            soul.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CalizRange"))
        {
            soulGoCaliz = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CalizRange"))
        {
            soulGoCaliz = false;
        }
    }
}
