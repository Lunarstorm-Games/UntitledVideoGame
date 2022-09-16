using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Assets.scripts.CustomEditors.Waves
{
    [CustomEditor(typeof(WaveSpawner))]
    public class WaveControllerEditor : Editor
    {
        
        private WaveSpawner EditorTarget;
        private SerializedObject waveStrengthProp;
        private float waveStrength=0f;
        private void OnEnable()
        {
            // Link the SerializedProperty to the variable 
            waveStrengthProp.FindProperty(nameof(waveStrength));

        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            serializedObject.Update();
            EditorTarget = (WaveSpawner)target;
            if (GUILayout.Button("Add spawnpoints"))
            {
                EditorTarget.AddSpawnPoints();
            }
            EditorGUILayout.LabelField("Wave Strength");
            waveStrength = EditorGUILayout.FloatField(waveStrength);
            if (GUILayout.Button("SpawnWave"))
            {
                EditorTarget.SpawnWave(waveStrength);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}