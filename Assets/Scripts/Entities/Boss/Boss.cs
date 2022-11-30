
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Entity
{

    [SerializeField] public float Speed = 3f;
    [SerializeField] public float AggroRange = 6f;
    [SerializeField] public float AttackRange = 6f;
    [SerializeField] public float AttackDelay = 3f;
    [SerializeField] public float PreAttackDelay = 1f;
    [SerializeField] public float Damage = 10f;

    public NavMeshAgent Agent { get; protected set; }
    public Entity CurrentTarget
    {
        get
        {
            if (currentTarget != null && currentTarget.gameObject.activeInHierarchy)
                return currentTarget;
            return null;
        }
        set { currentTarget = value; }
    }
    protected Entity currentTarget;

    public override void Awake()
    {
        base.Awake();

        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = Speed;
        Agent.stoppingDistance = AttackRange - 0.2f;
    }
}
