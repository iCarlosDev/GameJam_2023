using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySoul : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float minModifier;
    [SerializeField] private float maxModifier;

    private Vector3 velocity = Vector3.zero;
    [SerializeField] private bool isFollowing;
    
    //GETTERS && SETTERS//
    public Transform Target
    {
        get => target;
        set => target = value;
    }

    ////////////////////////////////

    public void StartFollowing()
    {
        isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref velocity, Time.deltaTime * Random.Range(minModifier, maxModifier));
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SoulDestination"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
