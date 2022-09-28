using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Assets.Scripts.SaveSystem
{
    public class PrefabSpawner
    {
        private static PrefabSpawner _instance;

        public static PrefabSpawner Instance
        {
            get
            {
                if (_instance == null) _instance = new PrefabSpawner();
                return _instance;
            }
        }
        public void FindAssets()
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab");
        }

    }
}
