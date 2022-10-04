using BehaviorTree;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts.Logic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity, IDamageable
{
    [SerializeField] private float health = 30f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float aggroRange = 6f;
    [SerializeField] private float attackRange = 2.4f;
    [SerializeField] private float attackDelay = 3f;
    [SerializeField] private float preAttackDelay = 1f;
    [SerializeField] private int essenceDropAmount = 10;

    public Target CurrentTarget { get; set; }
    public GameObject DamagedByGO { get; set; }

    public Animator Animator { get; protected set; }
    public NavMeshAgent Agent { get; protected set; }
    public Transform PlayerPos { get; protected set; }
    public float Health { get => health; protected set => health = value; }
    public float Damage { get => damage; protected set => damage = value; }
    public float Speed { get => speed; protected set => speed = value; }
    public float AggroRange { get => aggroRange; protected set => aggroRange = value; }
    public float AttackRange { get => attackRange; protected set => attackRange = value; }
    public float AttackDelay { get => attackDelay; protected set => attackDelay = value; }
    public float PreAttackDelay { get => preAttackDelay; protected set => preAttackDelay = value; }
    public int EssenceDropAmount { get => essenceDropAmount; protected set => essenceDropAmount = value; }
     
    [SerializeReference]public EssenceSourceLogic EssenceSource= new ();

    protected float currentHealth = 0f;
    protected bool death;


    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;

        

        Agent.speed = Speed;
        Agent.stoppingDistance = AttackRange * 0.85f;
        currentHealth = Health;
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
        EssenceSource.DropEssence();
    }

    public virtual void TakeDamage(float damage, Entity entity)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            DamagedByGO = entity.gameObject;
        }
        else if (!death)
        {
            death = true;
            Animator.SetTrigger("Death");
            GetComponent<Collider>().enabled = false;
            Agent.isStopped = true;
        }
    }
}
