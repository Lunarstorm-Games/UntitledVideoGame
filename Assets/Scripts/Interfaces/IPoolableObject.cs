using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IPoolableObject
    { 

        Action<GameObject> OnSetInactive { get; set; }
        string PrefabName { get; set; }
        void Initialize(Vector3 position, Quaternion rotation);
        void Initialize(Vector3 position);
    }
}
