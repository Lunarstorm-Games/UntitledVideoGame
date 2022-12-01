using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interfaces;
using Assets.Scripts.SaveSystem;
using UnityEngine;

public class BuildableStructure : PersistableMonoBehaviour, IInteractable, IDamageable
{
    public int EssenceCost = 0;
    public float health = 300;
    public void Interact(GameObject source)
    {
    }

    // Update is called once per frame
    protected void Update()
    {
        if (health < 0)
        {
            gameObject.SetActive(false);
        }
    }


    public override void OnLoad()
    {
        
    }

    public void TakeDamage(float damage, Entity origin)
    {
        if (health > 0)
        {

        health-=damage;
        }
       
    }
}
