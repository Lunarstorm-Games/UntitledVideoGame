using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Texture2D cursorArrow;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }
}
