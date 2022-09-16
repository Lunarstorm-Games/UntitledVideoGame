using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    //Default states of an enemy
    public ChaseState ChaseState { get; protected set; }
    public AttackState AttackState { get; protected set; }
    public SpawnState SpawnState { get; protected set; }


    public Animator Animator { get; protected set; }
    public NavMeshAgent Agent { get; protected set; }
    public FiniteStateMachine FSM { get; protected set; }
    public Target[] TargetInterests { get; protected set; }


    [SerializeField] protected EnemyData enemyData;

    [HideInInspector] public GameObject CurrentTarget;


    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        TargetInterests = GameObject.FindObjectsOfType<Target>();

        //StateMachine
        FSM = new FiniteStateMachine();
        SpawnState = new SpawnState(this, FSM, enemyData);
        ChaseState = new ChaseState(this, FSM, enemyData);
        AttackState = new AttackState(this, FSM, enemyData);
    }

    public virtual void Start()
    {
        FSM.InitializeState(SpawnState);
    }

    public virtual void Update()
    {
        FSM.CurrentState.LogicUpdate();
    }

    public virtual void LateUpdate()
    {
        FSM.CurrentState.PhysicsUpdate();
    }

    public virtual void TakeDamage(float damage)
    {
        enemyData.health -= damage;

        if (enemyData.health <= 0)
        {
            DropEssence();
            Animator.SetTrigger("Death");
            Destroy(this.gameObject);
        }
        else
        {
            Animator.SetTrigger("Damaged");
        }
    }

    protected void DropEssence()
    {
        //Essence system task
    }

}
