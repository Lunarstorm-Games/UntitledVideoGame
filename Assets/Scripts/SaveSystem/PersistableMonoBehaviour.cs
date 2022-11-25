using Assets.Scripts.Utility;
using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    public abstract class PersistableMonoBehaviour : MonoBehaviour
    {
        public string Id = null;
        [SaveField]
        public string prefabPath;
        [SaveField]
        public string objectName;
        [SaveField]
        public Vector3 localScale
        {
            get
            {
                return transform.localScale;
            }
            set
            {
                transform.localScale = value;
            }
        }
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
            objectName = gameObject.name;
            if (string.IsNullOrWhiteSpace(Id))
            {
                Id = Guid.NewGuid().ToString();
#if UNITY_EDITOR

                EditorUtility.SetDirty(this);
#endif
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
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
            Id = Guid.NewGuid().ToString();
        }
        public abstract void OnLoad();




    }
}