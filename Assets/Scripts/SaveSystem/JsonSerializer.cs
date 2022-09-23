using System.Collections;
using System.IO;
using Newtonsoft.Json;

namespace Assets.Scripts.SaveSystem
{
    public class JsonSerializer :ISerialize
    {
        public void Serialize(Hashtable data, string filepath)
        {
            
            File.WriteAllText(filepath+".json",JsonConvert.SerializeObject(data));
            
        }

        public Hashtable DeSerialize(string filepath)
        {
            return JsonConvert.DeserializeObject<Hashtable>(File.ReadAllText(filepath+".json"));
        }
    }
}