using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SaveSystem;
using UnityEngine;

[Serializable]
public class BasePersistable
{

    [SerializeField] public bool PersistLocation = false;
    [SerializeField] public bool PersistRotation = false;
    public Vector3? Location = null;
    public Quaternion? Rotation = null;
    [SerializeField] public string Id = Guid.NewGuid().ToString();

    public void InitializePersistance()
    {
        //if (Id == Guid.Empty) Id = Guid.NewGuid();
        //if (!StateStore.Instance.HasObject(this))
        //{
        //    StateStore.Instance.RegisterObject(this);
        //}

    }
    public void InitializePersistance(GameObject source)
    {
        if (PersistLocation)
        {
            Location = source.transform.position;
        }

        if (PersistRotation)
        {
            Rotation = source.transform.rotation;
        }
        InitializePersistance();

    }

}
