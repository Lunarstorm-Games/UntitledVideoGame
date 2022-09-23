using System.Collections;

namespace Assets.Scripts.SaveSystem
{
    public interface ISerialize
    {
        public void Serialize(Hashtable data, string filepath);
        public Hashtable DeSerialize(string filepath);
    }
}