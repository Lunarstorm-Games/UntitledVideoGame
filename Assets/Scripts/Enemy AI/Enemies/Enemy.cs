using System;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts.Logic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : EntityAI
{
    [Header("Essence System")]
    [SerializeReference] public EssenceSourceLogic EssenceSource = new();

    public Entity HitByTarget { get; set; }

    [Obsolete]
    public virtual void DropEssence()
    {
        
    }

    public override void TakeDamage(float damage, Entity entity)
    {
        if (!Killable)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0 && !Death)
        {
            OnDeath?.Invoke();
            Death = true;
        }
        else
        {
            HitByTarget = entity;
        }
    }

    
    protected void OnDestroy()
    {
        EssenceSource.DropEssence();
    }
}
