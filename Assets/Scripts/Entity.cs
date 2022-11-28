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
    [Header("Target System", order = 0)]
    [SerializeField] public EntityType EntityType;
    [Tooltip("Selecting the first target will make it the highest priority")]
    [SerializeField] public EntityType TargetInterests;
    [Tooltip("Position on this entity that can be targeted")]
    [SerializeField] public List<Transform> TargetSpots;
    

    [Header("Health System", order = 1)]
    [SerializeField] public bool Killable = true;
    [SerializeField] public float MaxHealth = 100f;
    [SerializeField] protected float currentHealth = 0f;
    [SerializeField] public UnityEvent OnDamageTaken;
    [SerializeField] public UnityEvent OnDeath;

    public Animator Animator { get; protected set; }
    public bool Death { get; protected set; }


    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
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
        if (!Killable) 
            return;

        currentHealth -= damage;

        if (currentHealth <= 0 && !Death)
        {
            Death = true;
            OnDeath?.Invoke();
        }
    }

    public virtual void DeathAnimEvent()
    {
        Destroy(this.gameObject);
    }

    public bool ValidTarget(EntityType target)
    {
        return TargetInterests.ToString().Contains(target.ToString());
    }
}

