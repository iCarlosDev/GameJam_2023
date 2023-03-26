using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu_LevelManager : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;
 

    [Header("--- MAIN MENU ---")]
    [Space(10)] 
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject playBTN;

    [Header("--- OPTIONS ---")]
    [Space(10)]
    [SerializeField] private GameObject optionsCanvas;
    [SerializeField] private GameObject backBTN;

    private void Awake()
    {
        _eventSystem = FindObjectOfType<EventSystem>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        _eventSystem.SetSelectedGameObject(playBTN);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void OpenMainMenu()
    {
        optionsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
        _eventSystem.SetSelectedGameObject(playBTN);
    }
    
    public void OpenOptions()
    {
        mainMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
        _eventSystem.SetSelectedGameObject(backBTN);
    }
}
