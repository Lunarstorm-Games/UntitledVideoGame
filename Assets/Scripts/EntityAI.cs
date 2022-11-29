using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using BehaviorTreeVincent;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EntityAI : Entity, ISlowable
{
    [SerializeField] protected float speed = 3f;
    [SerializeField] protected float aggroRange = 6f;
    [SerializeField] protected float attackRange = 2.4f;
    [SerializeField] protected float attackDelay = 3f;
    [SerializeField] protected float preAttackDelay = 1f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float stoppingDistance = 2f;

    public float Speed { get => speed; set => speed = value; }
    public float AggroRange { get => aggroRange; set => aggroRange = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public float AttackDelay { get => attackDelay; set => attackDelay = value; }
    public float PreAttackDelay { get => preAttackDelay; set => preAttackDelay = value; }
    public float Damage { get => damage; set => damage = value; }
    public float StoppingDistance { get => stoppingDistance; set => stoppingDistance = value; }

    public Entity CurrentTarget { get; protected set; }

    private NavMeshAgent agent;

    public override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Slow(float slowRate, Entity origin)
    {
        agent.speed -= slowRate;
    }
}
