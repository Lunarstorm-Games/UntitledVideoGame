using System;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts.Logic;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : EntityAI, IPoolableObject
{
    [Header("Essence System")]
    [SerializeReference] public EssenceSourceLogic EssenceSource = new();

    public Entity HitByTarget { get; set; }

    public Action<GameObject> OnSetInactive { get; set; }
    public string PrefabName { get; set; }

    [Obsolete]
    public virtual void DropEssence()
    {
        
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        transform.rotation = rotation;
        Initialize(position);
    }

    public void Initialize(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public override void TakeDamage(float damage, Entity entity)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !Death)
        {
            Death = true;
        }
        else
        {
            HitByTarget = entity;
        }
    }

    public override void DeathAnimEvent()
    {
        EssenceSource.DropEssence();
        if (OnSetInactive!=null)
        {
            OnSetInactive(gameObject);

            var collider = GetComponent(typeof(Collider)) as Collider;
            collider.enabled = true;
            gameObject.SetActive(false);
        }
        else
            Destroy(gameObject);
    }
}
