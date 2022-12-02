using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Texture2D cursorArrow;
    
    public void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void PlayGame()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene("Main scene", LoadSceneMode.Single);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("Options", LoadSceneMode.Single);
    }

    public void OpenDiscord()
    {
        Application.OpenURL("https://discord.com/");
    }

    public void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/LunarstormGames");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
