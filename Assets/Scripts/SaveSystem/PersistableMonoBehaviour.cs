using Assets.Scripts.Utility;
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    public class PersistableMonoBehaviour :MonoBehaviour
    {
        public string Id = null;
        public string PrefabId;
        public GameObject prefab;
        public SaveSettings SaveSettings = new SaveSettings();

        protected void Start()
        {
            if (!SaveManager.Instance.HasObject(Id))
            {
                SaveManager.Instance.RegisterObject(this);
            }
        }
        
        private void Awake()
        {
            if ( Id == null)
            {
                Id = Guid.NewGuid().ToString();
                EditorUtility.SetDirty(this);
            }
        }
        private void OnEnable()
        {
            
        }


        protected void LoadObject<TMono>() where TMono:PersistableMonoBehaviour
        {
            PersistableMonoBehaviour state= SaveManager.Instance.GetState<TMono>(Id);
        }
    }
}