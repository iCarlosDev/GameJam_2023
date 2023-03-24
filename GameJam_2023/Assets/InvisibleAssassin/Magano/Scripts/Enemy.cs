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

    void Start()
    {
        _animator = GetComponent<Animator>();
        _mesh = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        RestarVida();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetBool("IsHitting", true);
            _animator.SetBool("IsHitting_2", true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        _animator.SetBool("IsHitting", false);
        _animator.SetBool("IsHitting_2", false);
    }

    private void RestarVida()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            vida -= 50;
            Die();
        }
    }

    private void Die()
    {
        if (vida <= 0)
        {
            fx_polvo.SetActive(false);
            _animator.enabled = false;
            _mesh.speed = 0f;
            

        }
    }
}
