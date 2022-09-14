using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    protected enum EnemyState
    {
        Patrol,
        Chase,
        Attack,
    }

    public Animator Animator { get; protected set; }
    public NavMeshAgent Agent { get; protected set; }

    [SerializeField] protected float health = 30f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float speed = 3f;
    [SerializeField] protected float aggroRange = 15f;
    [SerializeField] protected float attackRange = 4f;
    [SerializeField] protected float attackDelay = 4f;
    [SerializeField] protected int essenceDropAmount = 10;
    [SerializeField] protected EnemyState enemyState = EnemyState.Patrol;

    protected float currentAttackDelay;
    protected EnemyState lastState;

    public GameObject player;
    public GameObject villageCenter;

    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
        villageCenter = GameObject.FindGameObjectWithTag("VillageCenter");

        Agent.speed = speed;
    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        switch (enemyState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Chase:
                Chase();
                break;

            case EnemyState.Attack:
                Attack();
                break;

            default:
                Debug.LogError("No EnemyState Found");
                return;
        }
    }

    protected virtual void Attack()
    {
        currentAttackDelay -= Time.deltaTime;
        if (currentAttackDelay <= 0f)
        {
            currentAttackDelay = attackDelay;
            Animator.SetTrigger("Attacking");
        }
        if (!InAttackRange())
        {
            SetState(EnemyState.Chase);
            return;
        }
    }

    protected virtual void Chase()
    {
        Animator.SetFloat("Speed", 1);
        Agent.SetDestination(player.transform.position);

        if (!LineOfSight())
        {
            SetState(EnemyState.Patrol);
            return;
        }
        if (InAttackRange())
        {
            SetState(EnemyState.Patrol);
            return;
        }
    }

    protected virtual void Patrol()
    {
        Animator.SetFloat("Speed", 1);
        Agent.SetDestination(villageCenter.transform.position);

        if (LineOfSight())
        {
            SetState(EnemyState.Chase);
            return;
        }
        if (InAttackRange())
        {
            SetState(EnemyState.Patrol);
            return;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DropEssence();
            Animator.SetTrigger("Dead");
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




    protected virtual bool LineOfSight()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance <= aggroRange)
        {
            return true;
        }
        return false;    
    }

    protected virtual bool InAttackRange()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance <= attackRange)
        {
            return true;
        }
        return false;
    }

    protected void SetState(EnemyState newState)
    {
        lastState = enemyState;
        enemyState = newState;
    }
}
