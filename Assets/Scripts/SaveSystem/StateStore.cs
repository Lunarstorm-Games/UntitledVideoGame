using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    public class StateStore
    {
        private static StateStore _instance;
        
        private Hashtable _states = new Hashtable();
        private ISerialize _serializer = new JsonSerializer();
        public static StateStore Instance
        {
            get
            {
                if(_instance == null) _instance = new StateStore();
                return _instance;
            }
        }

        public void RegisterObject(GameObject toPersist)
        {
            var id = toPersist.GetComponent<PersistableMonoBehaviour>().Id;
            _states.Add(id,toPersist);
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
            foreach (DictionaryEntry dictionaryEntry in _states)
            {
                //todo get components assignable from persistable monob
                var props2 = dictionaryEntry.Value.GetType().GetFields()
                    .Where(x => Attribute.IsDefined(x, typeof(PersistanceAttribute)));

                var props = dictionaryEntry.Value.GetType().GetFields()
                    .Where(x => Attribute.IsDefined(x, typeof(PersistanceAttribute)))
                    .ToDictionary(e => e.Name, e => e.GetValue(dictionaryEntry.Value));
                statesToSave.Add(dictionaryEntry.Key,props);
            }
            _serializer.Serialize(statesToSave,filePath);
        }

        public void LoadSave(string filePath)
        {
            var data = _serializer.DeSerialize(filePath);
            var persistableGameobjects = GameObject.FindGameObjectsWithTag("Persistable").ToHashSet();

            foreach (DictionaryEntry dictionaryEntry in data)
            {
              
            }
        }
    }
}