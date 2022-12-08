using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoOptions : MonoBehaviour
{
    [SerializeField] private Slider resolutionSlider;
    [SerializeField] private Slider displaySlider;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TextMeshProUGUI resolutionValue;
    [SerializeField] private TextMeshProUGUI displayValue;
    [SerializeField] private TextMeshProUGUI brightnessValue;

    private Resolution[] resolutions;
    private List<Resolution> availableScreenRes = new List<Resolution>();
    
    void Start()
    {
        GetResolutions();
        displayValue.text = DefaultDisplayMode();
    }

    private void GetResolutions()
    {
        resolutions = Screen.resolutions;

        for (int i = resolutions.Length - 1; i >= resolutions.Length - 6; i--)
        {
            if (i == resolutions.Length - 1)
            {
                Debug.Log("Highest resolution available: " + resolutions[i]);
            }
            
            availableScreenRes.Add(resolutions[i]);
        }
        availableScreenRes.Reverse();
        SetResolution(5f);
        resolutionSlider.value = 5f;
    }

    private string DefaultDisplayMode()
    {
        string displayName = "Unknown";
        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.Windowed:
                displayName = "Windowed";
                displaySlider.value = 0;
                break;
            case FullScreenMode.FullScreenWindow:
                displayName = "Borderless Window";
                displaySlider.value = 1;
                break;
            case FullScreenMode.ExclusiveFullScreen:
                displayName = "Fullscreen";
                displaySlider.value = 2;
                break;
        }

        return displayName;
    }

    public void SetResolution(float resIndex)
    {
        Resolution desiredRes = availableScreenRes[(int)resIndex];
        Screen.SetResolution(desiredRes.width, desiredRes.height, Screen.fullScreenMode);
        resolutionValue.text = desiredRes.ToString();
    }

    public void SetDislayMode(float displayIndex)
    {
        switch (displayIndex)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                displayValue.text = "Windowed";
                break;

            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                displayValue.text = "Borderless Window";
                break;
            
            case 2:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                displayValue.text = "Fullscreen";
                break;
        }
        Debug.Log(Screen.fullScreenMode);
    }
}
