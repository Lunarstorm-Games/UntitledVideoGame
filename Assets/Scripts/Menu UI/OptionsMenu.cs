using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    public Button[] Buttons;
    public GameObject[] Screens;

    private Button activeButton;
    private GameObject activeGO;
    public void Start()
    {
        Buttons[0].interactable = false;
        activeButton = Buttons[0];
        activeGO = Screens[0];
    }

    public void ShowVideoOptions()
    {
        activeGO.gameObject.SetActive(false);
        activeButton.interactable = true;
        Screens[0].gameObject.SetActive(true);
        activeGO = Screens[0];
        Buttons[0].interactable = false;
        activeButton = Buttons[0];
    }

    public void ShowAudioOptions()
    {
        activeGO.gameObject.SetActive(false);
        activeButton.interactable = true;
        Screens[1].gameObject.SetActive(true);
        activeGO = Screens[1];
        Buttons[1].interactable = false;
        activeButton = Buttons[1];
    }

    public void ShowControlOptions()
    {
        activeGO.gameObject.SetActive(false);
        activeButton.interactable = true;
        Screens[2].gameObject.SetActive(true);
        activeGO = Screens[2];
        Buttons[2].interactable = false;
        activeButton = Buttons[2];
    }

    public void ShowLanguageOptions()
    {
        activeGO.gameObject.SetActive(false);
        activeButton.interactable = true;
        Screens[3].gameObject.SetActive(true);
        activeGO = Screens[3];
        Buttons[3].interactable = false;
        activeButton = Buttons[3];
    }

    public void GoBack() => SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

}