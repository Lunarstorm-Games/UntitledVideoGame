using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    public class SaveManager
    {
        private static SaveManager _instance;

        private Hashtable _states = new Hashtable();
        private ISerialize _serializer = new JsonSerializer();

        public static SaveManager Instance
        {
            get
            {
                if (_instance == null) _instance = new SaveManager();
                return _instance;
            }
        }

        public void RegisterObject(PersistableMonoBehaviour persistableMonoBehaviour)
        {
            if (!_states.ContainsKey(persistableMonoBehaviour.Id))
                _states.Add(persistableMonoBehaviour.Id, persistableMonoBehaviour);
        }
        public T GetState<T>(string guid)
        {
            if (_states.ContainsKey(guid))
            {
                return (T)_states[guid];
            }

            return default;
        }

        public bool HasObject(string guid)
        {
            return _states.ContainsKey(guid);
        }

        public void SaveStateToFile(string filePath)
        {
            Hashtable statesToSave = new Hashtable();
            FindPersistableGameObjects();
            foreach (DictionaryEntry dictionaryEntry in _states)
            {

                var fields = dictionaryEntry.Value.GetType().GetFields(BindingFlags.NonPublic |
                                                                       BindingFlags.Instance | BindingFlags.Public)
                    .Where(x => Attribute.IsDefined(x, typeof(SaveFieldAttribute)) && x.CanWrite());
                var props = dictionaryEntry.Value.GetType().GetProperties(BindingFlags.NonPublic |
                                                                          BindingFlags.Instance | BindingFlags.Public)
                    .Where(x => Attribute.IsDefined(x, typeof(SaveFieldAttribute)) && x.CanWrite);


                try
                {

                    var propdict = props
                        .ToDictionary(e => e.Name, e => e.GetValue(dictionaryEntry.Value));
                    var fieldDict = fields
                        .ToDictionary(e => e.Name, e => e.GetValue(dictionaryEntry.Value));
                    foreach (var (key, value) in fieldDict)
                    {
                        if (!propdict.ContainsKey(key)) propdict.Add(key, value);
                    }

                    statesToSave.Add(dictionaryEntry.Key, propdict);
                }
                catch (StackOverflowException e)
                {
                    Debug.LogError(
                        $"A saved property in object {dictionaryEntry.Value} caused a stackoverflowexception");
                }
            }

            _serializer.Serialize(statesToSave, filePath);
        }

        private void FindPersistableGameObjects()
        {

            var objects = GameObject.FindObjectsOfType<PersistableMonoBehaviour>();
            foreach (var item in objects)
            {
                RegisterObject(item);
            }
        }

        public void LoadSave(string filePath)
        {

            var data = _serializer.DeSerialize(filePath);
         
            foreach (DictionaryEntry dictionaryEntry in data)
            {
                var dict = dictionaryEntry.Value as Dictionary<string, object>;
                if (!_states.ContainsKey(dictionaryEntry.Key))
                {
                    if (!dict.ContainsKey(nameof(PersistableMonoBehaviour.prefabPath)))
                    {
                        Debug.LogWarning("no prefab path found for asset");
                        continue;
                    }
                    GameObject go = Resources.Load<GameObject>(dict[nameof(PersistableMonoBehaviour.prefabPath)].ToString());
                    if (go == null)
                    {
                        Debug.LogWarning($"could not find asset {dict[nameof(PersistableMonoBehaviour.prefabPath)]}");
                        continue;
                    }
                    var newobject = GameObject.Instantiate(go);
                    var script = newobject.GetComponent<PersistableMonoBehaviour>();
                    script.Id=dictionaryEntry.Key.ToString();
                    RegisterObject(script);
                }

                var target = _states[dictionaryEntry.Key];
                DictionaryToObject(dict, target);
               


            }
        }

        /// <summary>
        /// writes nested dictionary data to the target object
        /// </summary>
        /// <param name="dictionary">deserialized save game object dictionary</param>
        /// <param name="target">persistablemonobehaviour target</param>
        private void DictionaryToObject(Dictionary<string, object> dictionary, object target)
        {
            foreach (var item in dictionary)
            {
                var childDict = item.Value as Dictionary<string, object>;
                var property = target.GetType().GetProperty(item.Key, BindingFlags.NonPublic |
                                                                      BindingFlags.Instance | BindingFlags.Public);
                var field = target.GetType().GetField(item.Key, BindingFlags.NonPublic |
                                                                BindingFlags.Instance | BindingFlags.Public);


                try
                {

                    if (property != null)
                    {
                        var value = Convert.ChangeType(item.Value, property.PropertyType);
                        if (property.CanWrite)
                        {

                            property.SetValue(target, value);
                        }
                    }

                    if (field != null)
                    {
                        var value = Convert.ChangeType(item.Value, field.FieldType);
                        if (field.CanWrite())
                        {

                            field.SetValue(target, value);
                        }

                    }

                    continue;
                }
                catch (Exception e)
                {
                  
                }

                if (childDict is not null)
                {
                    object propObject;
                    if (property is not null)
                    {
                        propObject = property.GetValue(target);
                    }
                    else
                    {
                        propObject = field.GetValue(target);
                    }

                    DictionaryToObject(childDict, propObject);
                }
            }
        }

        public void RemoveObject(PersistableMonoBehaviour persistableMonoBehaviour)
        {
            if(_states.ContainsKey(persistableMonoBehaviour.Id))_states.Remove(persistableMonoBehaviour.Id);
        }
    }

}
