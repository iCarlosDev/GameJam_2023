using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject respawn;
    public GameObject enemy;
    public float TiempodeAparecer;
    public float TiempoRepeticiones;

    public bool IsEnter;
    
    
    void Start()
    {
        InvokeRepeating("CrearEnemigos", TiempodeAparecer, TiempoRepeticiones);
    }

    public void CrearEnemigos()
    {
        if (IsEnter == false)
        { 
            Instantiate(enemy, respawn.transform.position, Quaternion.identity);
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsEnter = false;
        }
    }
}
