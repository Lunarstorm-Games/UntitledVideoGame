using System;
using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    public class PersistableMonoBehaviour :MonoBehaviour
    {
        public string Id = Guid.NewGuid().ToString();
        public SaveSettings SaveSettings = new SaveSettings();

        protected void Start()
        {
            if (!StateStore.Instance.HasObject(Id))
            {
                StateStore.Instance.RegisterObject(gameObject);
            }
        }

        protected void LoadObject<TMono>() where TMono:PersistableMonoBehaviour
        {
            PersistableMonoBehaviour state= StateStore.Instance.GetState<TMono>(Id);
            
        }
    }
}