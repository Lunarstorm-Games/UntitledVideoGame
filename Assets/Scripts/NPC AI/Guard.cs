using Assets.scripts.Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Guard : NPC, IDamageable
{
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
    public bool Death { get; protected set; }
    public float CurrentAttackDelay { get; set; }
    public Animator Animator { get; protected set; }
    public NavMeshAgent Agent { get; protected set; }

    [Header("Stats")]
    [SerializeField] public float Health = 30f;
    [SerializeField] public float Speed = 3f;
    [SerializeField] public float AggroRange = 6f;
    [SerializeField] public float AttackRange = 2.4f;
    [SerializeField] public float AttackDelay = 3f;
    [SerializeField] public float PreAttackDelay = 1f;
    [SerializeField] public int EssenceDropAmount = 10;

    [Header("Events")]
    [SerializeField] public UnityEvent OnDamageTaken;
    [SerializeField] public UnityEvent OnDeath;
    [SerializeField] public UnityEvent OnPreAttackFinish;
    [SerializeField] public UnityEvent OnAttackFinish;

    [SerializeReference] public EssenceSourceLogic EssenceSource = new();
    [SerializeField] protected Entity currentTarget;
    protected float currentHealth = 0f;


    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();

        Agent.speed = Speed;
        currentHealth = Health;
    }

    public virtual void TakeDamage(float damage, Entity entity)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !Death)
        {
            OnDeath?.Invoke();
            Death = true;
        }
    }

    public virtual void DeathAnimEvent()
    {
        Destroy(this.gameObject);
    }

    public virtual void FinishedPreAttackAnimEvent()
    {
        OnPreAttackFinish?.Invoke();
    }

    public virtual void FinishedAttackAnimEvent()
    {
        OnAttackFinish?.Invoke();
    }
}
