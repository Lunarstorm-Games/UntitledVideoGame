using BehaviorTree;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts.Logic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity, IDamageable
{
    public Animator Animator { get; protected set; }
    public NavMeshAgent Agent { get; protected set; }
    public Transform PlayerPos { get; protected set; }
    public EnemyTree Tree { get; protected set; }
    public GameObject CurrentTarget { get; set; }

    [SerializeField] public float health = 30f;
    [SerializeField] public float damage = 10f;
    [SerializeField] public float speed = 3f;
    [SerializeField] public float aggroRange = 6f;
    [SerializeField] public float attackRange = 2.4f;
    [SerializeField] public float attackDelay = 3f;
    [SerializeField] public float preAttackDelay = 1f;
     
    [SerializeReference]public EssenceSourceLogic EssenceSource= new ();

    protected float currentHealth = 0f;


    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;

        Tree = new EnemyTree(this);
        Tree.Initialize();

        Agent.speed = speed;
        Agent.stoppingDistance = attackRange * 0.85f;
        currentHealth = health;
    }


    public virtual void Update()
    {
        Tree.Evaluate();
    }


    public virtual void DeathAnimFinished()
    {
        DropEssence();
        Destroy(this.gameObject);
    }

    public virtual void DropEssence()
    {
        EssenceSource.DropEssence();
    }

    public virtual void TakeDamage(float damage, Entity entity)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            if (entity != null && entity.gameObject.CompareTag("Player"))
            {
                CurrentTarget = entity.gameObject;
            }
        }
        else
        {
            Animator.SetTrigger("Death");
        }
    }
}
