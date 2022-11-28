using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using BehaviorTreeVincent;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EntityAI : Entity, ISlowable
{
    [Header("Stats", order = 1)]
    [SerializeField] public float Speed = 3f;
    [SerializeField] public float AggroRange = 6f;
    [SerializeField] public float AttackRange = 2.4f;
    [SerializeField] public float AttackDelay = 3f;
    [SerializeField] public float PreAttackDelay = 1f;
    [SerializeField] public float Damage = 10f;
   
    [Header("Attack System", order = 3)]
    [SerializeField] public UnityEvent OnPreAttackFinish;
    [SerializeField] public UnityEvent OnAttackFinish;


    public NavMeshAgent Agent { get; protected set; }
    public float CurrentAttackDelay { get; set; }
    public Entity CurrentTarget
    {
        get
        {
            if (currentTarget != null && currentTarget.gameObject.activeInHierarchy)
                return currentTarget;
            return null;
        }
        set 
        {
            if (currentTarget != value)
            {
                targetSpot = null;
                currentTarget = value;
            }
        }
    }
    public Transform TargetSpot { 
        get 
        { 
            return targetSpot; 
        } 
        set 
        { 
            if (targetSpot == null) targetSpot = value; 
        } 
    }
    public BehaviourTree Tree { get; protected set; }
    
    [SerializeField] protected Entity currentTarget;
    [SerializeField] protected Transform targetSpot;



    public override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = Speed;
        Agent.stoppingDistance = AttackRange - 0.2f;
    }

    public virtual void FinishedPreAttackAnimEvent()
    {
        OnPreAttackFinish?.Invoke();
    }

    public virtual void FinishedAttackAnimEvent()
    {
        OnAttackFinish?.Invoke();
    }

    public void Slow(float slowRate, Entity origin)
    {
        Agent.speed -= slowRate;
    }
}
