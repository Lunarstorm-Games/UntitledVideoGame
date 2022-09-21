using BehaviorTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity, IDamageable
{
    public Animator Animator { get; protected set; }
    public NavMeshAgent Agent { get; protected set; }
    public Transform PlayerPos { get; protected set; }

    [HideInInspector] public GameObject CurrentTarget;

    [SerializeField] public float health = 30f;
    [SerializeField] public float damage = 10f;
    [SerializeField] public float speed = 3f;
    [SerializeField] public float aggroRange = 6f;
    [SerializeField] public float attackRange = 2.4f;
    [SerializeField] public float attackDelay = 3f;
    [SerializeField] public float preAttackDelay = 1f;
    [SerializeField] public int essenceDropAmount = 10;

    protected float currentHealth = 0f;

    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        
        Agent.speed = speed;
        Agent.stoppingDistance = attackRange * 0.85f;
        currentHealth = health;
    }


    public virtual void Update()
    {
        
    }


    public virtual void DeathAnimFinished()
    {
        DropEssence();
        Destroy(this.gameObject);
    }

    public virtual void DropEssence()
    {
        //Essence system task
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
