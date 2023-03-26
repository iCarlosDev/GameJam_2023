using System;
using System.Collections;
using UnityEngine;

public class CalizSouls : MonoBehaviour
{
    [SerializeField] private CalizSoulsPopUp _calizSoulsPopUp;
    
    [SerializeField] private int soulsGetted;

    private void Start()
    {
        if (_calizSoulsPopUp != null)
        {
            _calizSoulsPopUp.SoulStatsTMP.text = $"{soulsGetted} / {GameManager.instance.SoulsRequired}";
        }
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3f);
        VirtualCameraController.instance.VirtualCameraBeginAnimation();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Soul"))
        {
            if (soulsGetted < GameManager.instance.SoulsRequired)
            {
                Destroy(other.gameObject);
                soulsGetted++;
                _calizSoulsPopUp.SoulStatsTMP.text = $"{soulsGetted} / {GameManager.instance.SoulsRequired}";

                if (soulsGetted == GameManager.instance.SoulsRequired)
                {
                    Arena_LevelManager.instance.GetAllEnemiesInScene();

                    StartCoroutine(LoadScene());
                }
            }
        }
    }
}
