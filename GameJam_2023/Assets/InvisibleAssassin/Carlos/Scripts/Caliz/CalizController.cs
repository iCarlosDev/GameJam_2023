using System;
using UnityEngine;

public class CalizController : MonoBehaviour
{
    [SerializeField] private bool shouldStartArena;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && shouldStartArena)
        {
            StartArena();
        }
    }

    private void StartArena()
    {
        VirtualCameraController.instance.VirtualCameraBeginAnimation();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldStartArena = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldStartArena = false;
        }
    }
}
