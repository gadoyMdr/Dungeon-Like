using Scripts.Refactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PuzzleUI : MonoBehaviour
{
    [SerializeField]
    private InputAction pause;
    [SerializeField]
    private GameObject pauseMenu;


    private void Awake()
    {
        pause.performed += _ => TogglePauseMenu();
        pauseMenu.SetActive(false);
    }

    private void OnEnable()
    {
        pause.Enable();
    }

    private void OnDisable()
    {
        pause.Disable();
    }

    void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        if (pauseMenu.activeInHierarchy) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }

    void SaveAndGoMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
