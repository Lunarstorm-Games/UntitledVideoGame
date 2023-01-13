using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Interfaces;
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
    public TextMeshProUGUI MeleeSkeletonTracker;
    public TextMeshProUGUI MageSkeletonTracker;
    public TextMeshProUGUI BruteSkeletonTracker;
    public TextMeshProUGUI GoblinTracker;
    public TextMeshProUGUI OrcTracker;
    public TextMeshProUGUI LichTracker;
    public TextMeshProUGUI ElementTracker;
    public TextMeshProUGUI WaterTracker;
    public TextMeshProUGUI BossTracker;
    public GameObject Boss;
    
    public WaveSpawner waveSpawner;

    private void Awake()
    {
        MeleeSkeletonTracker.text = "???";
        MageSkeletonTracker.text = "???";
        BruteSkeletonTracker.text = "???";
        GoblinTracker.text = "???";
        OrcTracker.text = "???";
        LichTracker.text = "???";
        ElementTracker.text = "???";
        WaterTracker.text = "???";
        BossTracker.text = "???";
    }

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
        LoadMemoryBoard();
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
           
                if(enemy.Equals("Melee Skeleton") && !MeleeSkeletonTracker.text.Contains(enemy))
                {
                    MeleeSkeletonTracker.text = $"{enemy} first appeared at {hour}:{minutes.ToString().PadLeft(2, '0')}";
                }

                if (enemy == "Mage Skeleton" && !MageSkeletonTracker.text.Contains(enemy))
                {
                    MageSkeletonTracker.text = $"{enemy} first appeared at {hour}:{minutes.ToString().PadLeft(2, '0')}";
                }

                if (enemy == "Brute Skeleton" && !BruteSkeletonTracker.text.Contains(enemy))
                {
                    BruteSkeletonTracker.text =
                        $"{enemy} first appeared at {hour}:{minutes.ToString().PadLeft(2, '0')}";
                }

                if (enemy == "Goblin" && !GoblinTracker.text.Contains(enemy))
                {
                    GoblinTracker.text = $"{enemy} first appeared at {hour}:{minutes.ToString().PadLeft(2, '0')}";
                }

                if (enemy == "Orc" && !OrcTracker.text.Contains(enemy))
                {
                    OrcTracker.text = $"{enemy} first appeared at {hour}:{minutes.ToString().PadLeft(2, '0')}";
                }

                if (enemy == "Lich" && !LichTracker.text.Contains(enemy))
                {
                    LichTracker.text = $"{enemy} first appeared at {hour}:{minutes.ToString().PadLeft(2, '0')}";
                } 
                
                if (enemy == "EarthElemental" && !LichTracker.text.Contains(enemy))
                {
                    LichTracker.text = $"{enemy} first appeared at {hour}:{minutes.ToString().PadLeft(2, '0')}";
                } 
                
                if (enemy == "WaterElemental" && !LichTracker.text.Contains(enemy))
                {
                    LichTracker.text = $"{enemy} first appeared at {hour}:{minutes.ToString().PadLeft(2, '0')}";
                }

                if (Boss.activeInHierarchy)
                {
                    BossTracker.text = $"{enemy} first appeared at {hour}:{minutes.ToString().PadLeft(2, '0')}";
                }
                MemoryBoardSaver.SaveEnemyTrackerHistory(this);
        }
    }
    
    public void LoadMemoryBoard()
    {
        if (MemoryBoardSaver.CheckIfEnemyTrackerHistoryExists())
        {
            MemoryBoardData data = MemoryBoardSaver.LoadEnemyTrackerHistory();
            MeleeSkeletonTracker.text = data.meleeSkeletonHistory;
            MageSkeletonTracker.text = data.mageSkeletonHistory;
            BruteSkeletonTracker.text = data.bruteSkeletonHistory;
            GoblinTracker.text = data.goblinHistory;
            OrcTracker.text = data.orcHistory;
            LichTracker.text = data.lichHistory;    
        }
        else
        {
            MeleeSkeletonTracker.text = "???";
            MageSkeletonTracker.text = "???";
            BruteSkeletonTracker.text = "???";
            GoblinTracker.text = "???";
            OrcTracker.text = "???";
            LichTracker.text = "???";
        }
        
    }
}
