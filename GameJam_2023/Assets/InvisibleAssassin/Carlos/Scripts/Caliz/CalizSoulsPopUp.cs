using System;
using TMPro;
using UnityEngine;

public class CalizSoulsPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshPro soulStatsTMP;

    public TextMeshPro SoulStatsTMP => soulStatsTMP;

    private void Awake()
    {
        soulStatsTMP = transform.GetChild(0).GetComponent<TextMeshPro>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBase"))
        {
            soulStatsTMP.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerBase"))
        {
            soulStatsTMP.gameObject.SetActive(false);
        }
    }
}
