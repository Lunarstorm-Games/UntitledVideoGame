using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EntityAI : Entity, ISlowable
{
    [Header("AI Stats")]
    [SerializeField] protected float speed = 3f;
    [SerializeField] protected float aggroRange = 6f;
    [Tooltip("Make sure this value is higher than the stopping distance")]
    [SerializeField] protected float attackRange = 2.4f;
    [SerializeField] protected float attackDelay = 3f;
    [SerializeField] protected float attackSpeed = 1f;
    [SerializeField] protected float damage = 10f;
    [Tooltip("Make sure this value is lower than attack range")]
    [SerializeField] protected float stoppingDistance = 2f;

    public float Speed { get => speed; set => speed = value; }
    public float AggroRange { get => aggroRange; set => aggroRange = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public float AttackDelay { get => attackDelay; set => attackDelay = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float Damage { get => damage; set => damage = value; }
    public float StoppingDistance { get => stoppingDistance; set => stoppingDistance = value; }
    public Transform TargetSpot { get; set; }
    public Entity CurrentTarget { get; protected set; }

    private NavMeshAgent agent;

    public virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Slow(float slowRate, Entity origin)
    {
        agent.speed -= slowRate;
    }
}
