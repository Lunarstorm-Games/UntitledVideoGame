using System;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts.Models.WaveModels;
using TMPro;
using UniStorm;
using UnityEngine;
using UnityEngine.InputSystem;

public class MemoryBoardUIController : MonoBehaviour
{
    public PlayerInput playerInput;
    public GameObject memoryBoardPopUp;
    public GameObject memoryBoardUI;
    public GameObject pauseMenu;
    public GameObject EnemyTracker;
    public WaveSpawner waveSpawner;
    public List<EnemySpawnSetting> EnemyVariants = new List<EnemySpawnSetting>();
    

    void Update()
    {
        TrackEnemyAppearance();
        CheckPause();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 10f))
        {
            if (hit.collider.gameObject.CompareTag("MemoryBoard"))
            {
                if (memoryBoardUI.activeSelf == false && pauseMenu.activeSelf == false)
                {
                    ShowMemoryBoardPopUp();
                }
                if (Input.GetKey(KeyCode.E))
                {
                    ShowPanel();
                }
            }
        }else
        {
            HideMemoryBoardPopUp();
        }
    }
    
    public void ShowPanel()
    {
        memoryBoardUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        memoryBoardPopUp.SetActive(false);
        playerInput.actions.Disable();
    }
    
    public void HidePanel()
    {
        memoryBoardUI.SetActive(false);
        playerInput.actions.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void ShowMemoryBoardPopUp()
    {
        memoryBoardPopUp.SetActive(true);
    }
    
    public void HideMemoryBoardPopUp()
    {
        memoryBoardPopUp.SetActive(false);
    }
    
    public void CheckPause()
    {
        if (pauseMenu.activeSelf)
        {
            memoryBoardUI.SetActive(false);
            memoryBoardPopUp.SetActive(false);
        }
    }
    
    public void TrackEnemyAppearance()
    {
        int hour = UniStormSystem.Instance?.Hour??0;
        int minutes = UniStormSystem.Instance?.Minute??0;
        
        foreach (var enemy in waveSpawner.SpawnedEnemies)
        {
            EnemyVariants.Add(enemy);
        }
        
        foreach (var enemy in EnemyVariants)
        {
            if(!EnemyTracker.GetComponent<TextMeshProUGUI>().text.Contains(enemy.Prefab.gameObject.name))
                EnemyTracker.GetComponent<TextMeshProUGUI>().text += $"{enemy.Prefab.gameObject.name} first appeared at {hour}:{minutes.ToString().PadLeft(2, '0')}\n";
        }
    }
}
