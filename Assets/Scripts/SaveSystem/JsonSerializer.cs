using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Assets.Scripts.SaveSystem
{
    public class JsonSerializer : ISerialize
    {
        public void Serialize(Hashtable data, string filepath)
        {

            File.WriteAllText(filepath + ".json", JsonConvert.SerializeObject(data));

        }

        public Hashtable DeSerialize(string filepath)
        {
            var hashtable = new Hashtable();
            var root = JsonConvert.DeserializeObject<Hashtable>(File.ReadAllText(filepath + ".json"));
            foreach (DictionaryEntry item in root)
            {
                var dict = DeserializeChild(item.Value);
                if (dict == null)
                {
                hashtable.Add(item.Key, item.Value);

                }
                else
                {
                    hashtable.Add(item.Key, dict);
                }


            }
            return hashtable;
        }
        private Dictionary<string, object> DeserializeChild(object value)
        {
            string json = value.ToString();
            try
            {

                var returnvalue = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                var copy = new Dictionary<string, object>(returnvalue);
                foreach(var item in copy)
                {
                    var child = DeserializeChild(item.Value);
                    if(child != null)
                    {
                        returnvalue[item.Key] = child;
                    }
                }

                return returnvalue;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public T DeserializeItem<T>(object source)
        {
            try
            {

                return JsonConvert.DeserializeObject<T>(source.ToString());
            }
            catch
            {
                return default;
            }
        }
        public dynamic DeserializeItem(object source, Type type)
        {
            try
            {

                return JsonConvert.DeserializeObject<dynamic>(source.ToString());
            }
            catch (Exception e)
            {
                return default;
            }
        }
    }
}