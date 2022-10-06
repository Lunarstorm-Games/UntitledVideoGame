using BehaviorTree;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts.Logic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : Entity, IDamageable
{
    [SerializeField] public float Health = 30f;
    [SerializeField] public float Speed = 3f;
    [SerializeField] public float AggroRange = 6f;
    [SerializeField] public float AttackRange = 2.4f;
    [SerializeField] public float AttackDelay = 3f;
    [SerializeField] public float PreAttackDelay = 1f;
    [SerializeField] public int EssenceDropAmount = 10;
    [SerializeReference] public EssenceSourceLogic EssenceSource = new();
    [SerializeField] public UnityEvent OnDamageTaken;
    [SerializeField] public UnityEvent OnDeath;

    [HideInInspector] public Entity CurrentTarget;
    [HideInInspector] public Entity HitByTarget;
    [HideInInspector] public Animator Animator;
    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public bool Death;

    protected float currentHealth = 0f;
    


    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();  

        Agent.speed = Speed;
        Agent.stoppingDistance = AttackRange * 0.85f;
        currentHealth = Health;
    }

    public virtual void DropEssence()
    {
        EssenceSource.DropEssence();
    }

    public virtual void TakeDamage(float damage, Entity entity)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !Death)
        {
            OnDeath?.Invoke();
            Death = true;
            GetComponent<Collider>().enabled = false;
            Agent.isStopped = true;
            Animator.SetTrigger("Death");
        }
        else
        {

            HitByTarget = entity;
        }
    }

    public virtual void DeathAnimEvent()
    {
        DropEssence();
        Destroy(this.gameObject);
    }
}
