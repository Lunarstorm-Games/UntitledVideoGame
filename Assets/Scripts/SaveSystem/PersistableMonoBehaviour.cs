using Assets.Scripts.Utility;
using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    public class PersistableMonoBehaviour :MonoBehaviour
    {
        public string Id = null;
        [SaveField]
        public string prefabPath;

        [SaveField]
        public Vector3 position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

        [SaveField]
        public Quaternion rotation
        {
            get
            {
                return transform.rotation;
            }
            set
            {
                transform.rotation = value;
            }
        }

        protected void Start()
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                Id = Guid.NewGuid().ToString();
                EditorUtility.SetDirty(this);
            }
            if (!SaveManager.Instance.HasObject(Id))
            {
                SaveManager.Instance.RegisterObject(this);
            }
        }
        


        protected void OnDestroy()
        {
            SaveManager.Instance.RemoveObject(this);
        }

        [ContextMenu("Clear Id")]
        public void ClearId()
        {
            Id = null;
        }

        [ContextMenu("Generate Id")]
        public void GenerateId()
        {
            Id= Guid.NewGuid().ToString();
        }


       
    }
}