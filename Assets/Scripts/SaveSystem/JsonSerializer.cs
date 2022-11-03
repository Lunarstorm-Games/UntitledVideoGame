using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

namespace Assets.Scripts.SaveSystem
{
    public class JsonSerializer : ISerialize
    {
        public JsonSerializer()
        {
            JsonConvert.DefaultSettings = () =>
            {
                return new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.All
                };
            };
        }
        public void Serialize(Hashtable data, string filepath)
        {
            File.WriteAllText(filepath + ".json", JsonConvert.SerializeObject(data));
        }

        public Hashtable DeSerialize(string filepath)
        {
            var hashtable = new Hashtable();
            var root = JsonConvert.DeserializeObject(File.ReadAllText(filepath + ".json"));
      
            return root as Hashtable;
        }

        public void DeleteFile(string filepath)
        {
            File.Delete(filepath+".json");
        }
    }
}