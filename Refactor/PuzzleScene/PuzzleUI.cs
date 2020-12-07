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

    private BoardBootstrapper currentBoardBootstraper;
    private GameTimer currentGameTimer;
    private PlayerMovementsSaver currentPlayerMovements;

    private void Awake()
    {
        currentBoardBootstraper = GameObject.FindObjectOfType<BoardBootstrapper>();
        currentGameTimer = GameObject.FindObjectOfType<GameTimer>();
        currentPlayerMovements = GameObject.FindObjectOfType<PlayerMovementsSaver>();

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

    public void SaveAndGoMainMenu()
    {
        currentBoardBootstraper._puzzle.AddOrReplaceInfoField(PuzzleInfoField.playerMovements, currentPlayerMovements.playersMovement);
        currentBoardBootstraper._puzzle.AddOrReplaceInfoField(PuzzleInfoField.time, currentGameTimer.time.ToString());
        SceneManager.LoadScene("MainMenu");
    }
}
