using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum EntityType
{
    Player,
    NPC,
    Enemy,
    Structure,
}

[System.Flags]
public enum TargetInterests
{
    Player = 1,
    NPC = 2,
    Enemy = 4,
    Structure = 8,
}

public class Entity : MonoBehaviour, IDamageable
{
    [Header("Target System", order = 0)]
    [SerializeField] public EntityType Type;
    [SerializeField] public TargetInterests TargetsType;
    [Tooltip("Position for enemies to shoot at")]
    [SerializeField] private Vector3 TargetOffset = Vector3.zero;

    [Header("Health System", order = 1)]
    [SerializeField] public bool Killable = true;
    [SerializeField] public float Health = 30f;
    [SerializeField] public UnityEvent OnDamageTaken;
    [SerializeField] public UnityEvent OnDeath;

    public Animator Animator { get; protected set; }
    public bool Death { get; protected set; }


    private Vector3 Offset;
    protected float currentHealth = 0f;




    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        currentHealth = Health;
    }
    /// <summary>
    /// gets the transform.position with offset of the object.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetTargetOffset()
    {
        Offset = transform.position;
        Offset += TargetOffset;
        return Offset;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(GetTargetOffset(), 0.1f);
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
        return TargetsType.ToString().Contains(target.ToString());
    }
}

