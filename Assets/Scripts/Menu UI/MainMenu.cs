using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene("Map2", LoadSceneMode.Single);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenOptions() => SceneManager.LoadScene("OptionsMenu", LoadSceneMode.Single);

    public void OpenDiscord() => Application.OpenURL("https://discord.com/");

    public void OpenTwitter() => Application.OpenURL("https://twitter.com/LunarstormGames");
    
    public void QuitGame() => Application.Quit();

}
