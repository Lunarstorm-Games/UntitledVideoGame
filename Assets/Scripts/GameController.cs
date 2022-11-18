using Assets.Scripts.SaveSystem;
using Assets.Scripts.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniStorm;
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
        public WeatherType WeatherType;
        public GameObject TimerUi;
        public GameObject WaveController;
        public float PreAttackTimer = 300;
        private bool AttackHasStarted = false;
        public bool SkipPreAttackTimer = false;
        public string time ;


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
            if (Input.GetButtonUp(skipInputName) && !AttackHasStarted) UniStormManager.Instance.SetTime(19, 00);
            
        }

        private void UpdateTimer()
        {
            Debug.Log("Update Timer");
            int hour = UniStormSystem.Instance.Hour;
            int minutes = UniStormSystem.Instance.Minute;

            TimerUi.GetComponent<TextMeshProUGUI>().text = $"{hour}:{minutes.ToString().PadLeft(2, '0')}";
            if (hour == 19 && !AttackHasStarted)
            {
                WaveController.GetComponent<WaveSpawner>().StartWaves();
                //TimerUi.SetActive(false);
                AttackHasStarted = true;
            }

        }


        private void InitializeLevel()
        {
            if (SaveManager.Instance.SaveExists(LevelName))
            {
                SaveManager.Instance.LoadSave(LevelName);
            }
            if (SkipPreAttackTimer) PreAttackTimer = 1;
            UniStorm.UniStormManager.Instance.ChangeWeatherInstantly(WeatherType);
           
        }

        [ContextMenu("Clearsave")]
        public void ClearSave()
        {
            SaveManager.Instance.ClearSave(SceneManager.GetActiveScene().name);
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