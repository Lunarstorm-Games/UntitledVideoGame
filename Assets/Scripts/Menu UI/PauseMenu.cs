using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public GameObject pauseMenuUIl;
    public PlayerInput controls;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
            pauseMenuUIl.SetActive(false);
            Time.timeScale = 1f;
            controls.actions.Enable();
            IsGamePaused = false;
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        pauseMenuUIl.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        controls.actions.Disable();
    }

    public void LoadMenu()
    {
        IsGamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        MemoryBoardSaver.DeleteEnemyTrackerHistory();
        Application.Quit();
    }
}
