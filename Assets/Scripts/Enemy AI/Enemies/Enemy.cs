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
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
        EssenceSource.DropEssence();
        OnSetInactive(gameObject);
    }

    protected void OnDestroy()
    {
        Debug.LogWarning("Pooled enemy should not be destroyed");
    }
}
