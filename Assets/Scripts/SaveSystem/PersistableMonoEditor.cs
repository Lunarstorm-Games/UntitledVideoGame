#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SaveSystem;
using FullscreenEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
[CustomEditor((typeof(PersistableMonoBehaviour)))]
public class PersistableMonoEditor : Editor
{
    SerializedProperty idProperty;
    // Start is called before the first frame update
    private void OnEnable()
    {
        //var x= serializedObject.FindProperty(nameof(PersistableMonoBehaviour.Id));
        

    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
#endif