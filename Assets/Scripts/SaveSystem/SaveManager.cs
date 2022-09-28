using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            if (!_states.ContainsKey(persistableMonoBehaviour.Id)) _states.Add(persistableMonoBehaviour.Id, persistableMonoBehaviour);
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

                ////todo get components assignable from persistable monob
                //var fields = dictionaryEntry.Value.GetType().GetFields(BindingFlags.NonPublic |
                //         BindingFlags.Instance | BindingFlags.Public)
                //    .Where(x => Attribute.IsDefined(x, typeof(PersistanceAttribute)));

                var props = dictionaryEntry.Value.GetType().GetFields(BindingFlags.NonPublic |
                         BindingFlags.Instance | BindingFlags.Public)
                    .Where(x => Attribute.IsDefined(x, typeof(SaveFieldAttribute)));

                //var fieldDict= fields
                //    .ToDictionary(e => e.Name, e => e.GetValue(dictionaryEntry.Value));
                var propdict = props
                    .ToDictionary(e => e.Name, e => e.GetValue(dictionaryEntry.Value));

                //statesToSave.Add(dictionaryEntry.Key, fieldDict);
                statesToSave.Add(dictionaryEntry.Key, propdict);
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
            //var persistableGameobjects = GameObject.FindGameObjectsWithTag("Persistable").ToHashSet();
            //var persistableGameobjects = GameObject.FindObjectsOfType<PersistableMonoBehaviour>().ToHashSet();

            foreach (DictionaryEntry dictionaryEntry in data)
            {

                if (_states.ContainsKey(dictionaryEntry.Key))
                {
                    var dict = dictionaryEntry.Value as Dictionary<string, object>;
                    var target = _states[dictionaryEntry.Key];
                    DictionaryToObject(dict, target);
                    //foreach(DictionaryEntry item in dictionaryEntry.Value)
                }

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
                else
                {
                    try
                    {

                        if (property != null)
                        {
                            var value = Convert.ChangeType(item.Value, property.PropertyType);
                            property.SetValue(target, value);
                        }

                        if (field != null)
                        {
                            var value = Convert.ChangeType(item.Value, field.FieldType);
                            field.SetValue(target, value);

                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"unable to set property or field {property?.Name ?? field.Name} from save");
                    }
                }
            }
        }
        
    }
}