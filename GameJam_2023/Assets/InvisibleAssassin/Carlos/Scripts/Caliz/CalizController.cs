using System;
using UnityEngine;

public class CalizController : MonoBehaviour
{
    [SerializeField] private bool shouldStartArena;

    [SerializeField] private GameObject startWavePopUp;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && shouldStartArena)
        {
            StartArena();
            GameManager.instance.PlayerPosition = GameObject.FindWithTag("PlayerBase").transform.position;
            GameManager.instance.PlayerRotation = GameObject.FindWithTag("PlayerBase").transform.rotation;
        }
    }

    private void StartArena()
    {
        VirtualCameraController.instance.VirtualCameraBeginAnimation();
        shouldStartArena = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBase"))
        {
            shouldStartArena = true;
            startWavePopUp.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerBase"))
        {
            shouldStartArena = false;
            startWavePopUp.SetActive(false);
        }
    }
}
