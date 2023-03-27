using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _mesh;

    public GameObject fx_polvo;

    public int vida = 100;

    public Enemy_AI _enemyAI;

    [SerializeField] private SphereCollider r_SphereCollider;
    [SerializeField] private SphereCollider l_SphereCollider;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _mesh = GetComponent<NavMeshAgent>();
        _enemyAI = GetComponent<Enemy_AI>();

        vida = GameManager.instance.EnemyMaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Caliz"))
        {
            _animator.SetBool("IsHitting", true);
            _animator.SetBool("IsHitting_2", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_enemyAI.RangeCalizOkFlag == false)
        {
            if (other.CompareTag("Player"))
            {
                _animator.SetBool("IsHitting", false);
                _animator.SetBool("IsHitting_2", false); 
            }
        }
    }

    /*private void RestarVida()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            vida -= 50;
            
            if (vida <= 0)
            {
                Die();
            }
        }
    }*/

    public void Die()
    {
        fx_polvo.SetActive(false);
        _animator.enabled = false;
        _mesh.speed = 0f;

        GetComponent<SphereCollider>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        r_SphereCollider.enabled = false;
        l_SphereCollider.enabled = false;
            
        CalizHealth.instance.CheckEnemiesInRange(gameObject);

        StartCoroutine(DestroyEnemy());
    }

    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
