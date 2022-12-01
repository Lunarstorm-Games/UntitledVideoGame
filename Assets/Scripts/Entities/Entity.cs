using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


[System.Flags]
public enum EntityType
{
    None = 0,
    Player = 1,
    NPC = 2,
    Enemy = 4,
    Structure = 8,
    MainObjective = 16,
}

public class Entity : MonoBehaviour, IDamageable
{

    [Header("Targeting System")]
    [SerializeField] protected EntityType entityType;
    [Tooltip("Selecting the first target will make it the highest priority")]
    [SerializeField] protected EntityType targetInterests;
    [Tooltip("Position on this entity that can be targeted")]
    [SerializeField] protected List<Transform> targetSpots;

    [Header("Health System")]
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float currentHealth = 0f;
    [SerializeField] protected bool death;

    public EntityType EntityType { get => entityType; set => entityType = value; }
    public EntityType TargetInterests { get => targetInterests; set => targetInterests = value; }
    public List<Transform> TargetSpots { get => targetSpots; set => targetSpots = value; }
    
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool Death { get => death; set => death = value; }

    public virtual void OnEnable()
    {
        death = false;
        currentHealth = MaxHealth;
    }


    public Transform GetEntityTargetSpot()
    {
        if (TargetSpots.Count == 0 || TargetSpots == null) 
            return this.transform;

        int index = UnityEngine.Random.Range(0, TargetSpots.Count);
        return TargetSpots[index];
    }

    public virtual void TakeDamage(float damage, Entity origin)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !Death)
        {
            Death = true;
        }
    }

    public virtual void DeathAnimEvent()
    {
        Destroy(this.gameObject);
    }

    public bool IsValidTarget(EntityType type)
    {
        return targetInterests.ToString().Contains(type.ToString());
    }
}

