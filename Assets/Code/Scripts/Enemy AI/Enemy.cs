using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    //Default states of an enemy
    public FollowTargetState FollowTargetState { get; protected set; }
    public AttackState AttackState { get; protected set; }
    public SpawnState SpawnState { get; protected set; }
    public FindNewTargetState FindNewTargetState { get; protected set; }


    public Animator Animator { get; protected set; }
    public NavMeshAgent Agent { get; protected set; }
    public FiniteStateMachine FSM { get; protected set; }
    public Target[] TargetInterests { get; protected set; }


    [SerializeField] protected EnemyData enemyData;
    [SerializeField] public GameObject CurrentTarget;

    protected bool damageAnimFinished = true;
   


    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        

        //StateMachine
        FSM = new FiniteStateMachine();
        SpawnState = new SpawnState(this, FSM, enemyData);
        FollowTargetState = new FollowTargetState(this, FSM, enemyData);
        AttackState = new AttackState(this, FSM, enemyData);
        FindNewTargetState = new FindNewTargetState(this, FSM, enemyData);
    }

    public virtual void Start()
    {
        FSM.InitializeState(SpawnState);
    }

    public virtual void Update()
    {
        CheckTargetAlive();
        FSM.CurrentState.LogicUpdate();
    }

    public virtual void LateUpdate()
    {
        FSM.CurrentState.PhysicsUpdate();
    }

    public virtual void TakeDamage(float damage)
    {
        enemyData.currentHealth -= damage;

        if (enemyData.currentHealth <= 0)
        {
            Animator.SetTrigger("Death");
        }
        else if (damageAnimFinished)
        {
            damageAnimFinished = false;
            Animator.SetTrigger("Damaged");
        }
    }


    public virtual void DeathAnimFinished()
    {
        DropEssence();
        Destroy(this.gameObject);
    }

    public virtual void DamageAnimFinished()
    {
        damageAnimFinished = true;
    }

    public virtual void FindClosestTarget()
    {
        TargetInterests = GameObject.FindObjectsOfType<Target>();
        if (TargetInterests == null)
        {
            return;
        }

        Target closest = null;
        float distance = Mathf.Infinity;

        foreach (Target go in TargetInterests)
        {
            Vector3 diff = go.transform.position - this.transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        if (closest != null)
        {
            CurrentTarget = closest.gameObject;
        }

    }

    public virtual void Attack()
    {
        Debug.Log(enemyData.currentAttackDelay);
        enemyData.currentAttackDelay -= Time.deltaTime;
        if (enemyData.currentAttackDelay <= 0f)
        {
            enemyData.currentAttackDelay = enemyData.attackDelay;
            Animator.SetTrigger("Attacking");
        }
    }

    public virtual bool InAttackRange()
    {
        float distance = Vector3.Distance(this.transform.position, CurrentTarget.transform.position);
        Debug.Log(distance);
        if (distance < enemyData.attackRange)
        {
            return true;
        }
        return false;
    }

    public virtual void SetTargetDestination(Vector3 target)
    {
        Agent.SetDestination(target);
    }

    public virtual void CheckTargetAlive()
    {
        if (CurrentTarget == null || !CurrentTarget.activeInHierarchy)
        {
            FSM.ChangeState(FindNewTargetState);
        }
    }

    public virtual void LookAtTarget()
    {
        Vector3 dir = CurrentTarget.transform.position - this.transform.position;
        dir.y = 0;
        this.transform.rotation = Quaternion.LookRotation(dir);
    }

    public virtual void DropEssence()
    {
        //Essence system task
    }

}
