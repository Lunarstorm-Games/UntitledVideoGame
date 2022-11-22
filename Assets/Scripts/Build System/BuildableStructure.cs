using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interfaces;
using Assets.Scripts.SaveSystem;
using UnityEngine;

public class BuildableStructure : PersistableMonoBehaviour, IInteractable
{
    public int EssenceCost = 0;
    public void Interact(GameObject source)
    {
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


    public override void OnLoad()
    {
        
    }
}
