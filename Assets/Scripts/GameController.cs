using Assets.Scripts.SaveSystem;
using Assets.Scripts.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        [ReadOnly] public string LevelName;
        [ReadOnly] public string SaveDirectory;
        public GameObject TimerUi;
        public GameObject WaveController;
        public float PreAttackTimer = 300;
        private bool AttackHasStarted = false;
        public bool SkipPreAttackTimer = false;
        
        public string skipInputName = "startAttack";
        private bool timeLoopStarted = false;

        // Start is called before the first frame update
        void Start()
        {

            if (!instance) instance = this;
            else Destroy(gameObject);
            SaveDirectory = Application.persistentDataPath;
            LevelName = SceneManager.GetActiveScene().name;

            InitializeLevel();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateTimer();
            if (Input.GetButtonUp(skipInputName)) PreAttackTimer = 0;
        }

        private void UpdateTimer()
        {
            if (PreAttackTimer > 0) PreAttackTimer -= Time.deltaTime;
            else PreAttackTimer = 0;
            TimerUi.GetComponent<TextMeshProUGUI>().text = ((int)PreAttackTimer).ToString();
            if (PreAttackTimer == 0)
            {
                WaveController.GetComponent<WaveSpawner>().StartWaves();
                TimerUi.SetActive(false);
            }

        }

        private void InitializeLevel()
        {
            if (SaveManager.Instance.SaveExists(LevelName))
            {
                SaveManager.Instance.LoadSave(LevelName);
            }
            if (SkipPreAttackTimer) PreAttackTimer = 1;
        }

        [ContextMenu("StartTimeloop")]
        public void StartTimeLoop()
        {
            if (!timeLoopStarted)
            {
                timeLoopStarted = true;
                SaveManager.Instance.SaveStateToFile(LevelName);
                SceneManager.LoadScene(LevelName);
            }
        }

    }
}