using Assets.Scripts.SaveSystem;
using Assets.Scripts.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniStorm;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof(TimeLoopEffectManager))]
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        [ReadOnly] public string LevelName;
        [ReadOnly] public string SaveDirectory;
        public BuildModeController BuildModeController;
        public WeatherType WeatherType;
        public GameObject TimerUi;
        public GameObject WaveController;
        public float PreAttackTimer = 300;
        private bool AttackHasStarted = false;
        public bool SkipPreAttackTimer = false;
        public string time ;
        private TimeLoopEffectManager TimeLoopEffectManager;

        public string skipInputName = "startAttack";
        private bool timeLoopStarted = false;
        private void Awake()
        {
            SaveDirectory = Application.persistentDataPath;
            TimeLoopEffectManager = GetComponent<TimeLoopEffectManager>();
        }
        // Start is called before the first frame update
        void Start()
        {

            Time.timeScale = 1;
            if (!instance) instance = this;
            else Destroy(gameObject);
            
            LevelName = SceneManager.GetActiveScene().name;
            InitializeLevel();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateTimer();
            if (Input.GetButtonUp(skipInputName) && !AttackHasStarted)
            {

                UniStormManager.Instance.SetTime(19, 00);
                TimeLoopEffectManager.StartCapture();
            }
            
        }

        private void UpdateTimer()
        {
            int hour = UniStormSystem.Instance?.Hour??0;
            int minutes = UniStormSystem.Instance?.Minute??0;

            TimerUi.GetComponent<TextMeshProUGUI>().text = $"{hour}:{minutes.ToString().PadLeft(2, '0')}";
            if (hour == 19 && !AttackHasStarted)
            {
                BuildModeController.Disable();
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
                TimeLoopEffectManager.StopCapture();
                timeLoopStarted = true;

                SaveManager.Instance.SaveStateToFile(LevelName);
                var loadingTask= SceneManager.LoadSceneAsync(LevelName);
                loadingTask.allowSceneActivation = false;
                TimeLoopEffectManager.StartTimeloopEffect(()=>
                {
                    loadingTask.allowSceneActivation = true;
                    
                });

               

            }
        }
     
        [ContextMenu("GenerateIdsForPersistable")]
        public void GenerateIdsForPersistable()
        {
            ClearSave();
            GameObject.FindObjectsOfType<PersistableMonoBehaviour>().ToList().ForEach(x => x.GenerateId());
        }

    }
}