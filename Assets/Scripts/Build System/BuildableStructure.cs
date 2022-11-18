using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using Assets.Scripts.SaveSystem;
using UnityEngine;

public class BuildableStructure : PersistableMonoBehaviour, IInteractable
{
    public void Interact(GameObject source)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
           
    }
}
