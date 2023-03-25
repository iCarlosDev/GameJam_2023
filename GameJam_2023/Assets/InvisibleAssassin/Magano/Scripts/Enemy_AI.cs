using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
 public NavMeshAgent navMeshAgent;
 public Transform player;
 public Transform caliz;

 public FieldOfView _fieldOfView;

 public bool RangeCalizOkFlag;
 


 private void Awake()
 {
     navMeshAgent = GetComponent<NavMeshAgent>();
     _fieldOfView = GetComponent<FieldOfView>();
     player = GameObject.FindGameObjectWithTag("Player").transform;
     caliz = GameObject.FindGameObjectWithTag("Caliz").transform;
 }

 private void Start()
 {
     PerseguirCaliz();
 }

 void Update()
    {
        if (_fieldOfView.canSeePlayer == true && RangeCalizOkFlag == false)
        {
            PerseguirPlayer();
        }

        if (RangeCalizOkFlag)
        {
            PerseguirCaliz();
        }
    }
    
    private void PerseguirPlayer()
    {
        navMeshAgent.SetDestination(player.position);
    } 
    
    private void PerseguirCaliz()
    {
        navMeshAgent.SetDestination(caliz.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Caliz"))
        {
            RangeCalizOkFlag = true;
        }
    }
}
