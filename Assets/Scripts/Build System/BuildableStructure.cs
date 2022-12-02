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


    public override void OnLoad()
    {
        
    }

}
