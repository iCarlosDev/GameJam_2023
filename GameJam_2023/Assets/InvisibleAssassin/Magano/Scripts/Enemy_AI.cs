using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
 public NavMeshAgent navMeshAgent;
 public Transform player;
 public Transform caliz;

    
    void Start()
    {
        PerseguirPlayer();
    }
    
    void Update()
    {
        navMeshAgent.SetDestination(player.position);
    }
    
    private void PerseguirPlayer()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    } 
    
    private void PerseguirCaliz()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        caliz = GameObject.FindGameObjectWithTag("Caliz").transform;
    }

}
