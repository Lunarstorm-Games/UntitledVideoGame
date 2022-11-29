//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class TaskRangeAttack : Node
//{
//    protected Animator animator;
//    protected NavMeshAgent agent;
//    protected float currentAttackDelay;
//    protected float preAttackDelay;
//    protected float attackDelay;
//    protected float damage;
//    protected float projectileRange;
//    protected float projectileSpeed;
//    protected EntityAI entity;
//    protected Projectile projectilePrefab;
//    protected Transform projectileSpawnPos;

//    public TaskRangeAttack(EntityAI entity, Projectile projectile, Transform projectileSpawnPos)
//    {
//        this.animator = entity.Animator;
//        this.agent = entity.Agent;
//        this.attackDelay = entity.AttackDelay;
//        this.preAttackDelay = entity.PreAttackDelay;
//        this.entity = entity;
//        this.projectilePrefab = projectile;
//        this.projectileSpawnPos = projectileSpawnPos;

//        this.entity.OnPreAttackFinish.AddListener(FinishedPreAttackAnimEvent);
//        this.entity.OnAttackFinish.AddListener(FinishedAttackAnimEvent);
//    }

//    public override NodeState Evaluate()
//    {
//        Vector3 dir = entity.TargetSpot.position - entity.transform.position;
//        dir.y = 0;
//        entity.transform.rotation = Quaternion.LookRotation(dir);

//        animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

//        entity.CurrentAttackDelay -= Time.deltaTime;
//        if (entity.CurrentAttackDelay <= 0f && !animator.GetBool("PreAttack"))
//        {
//            animator.SetFloat("PreAttackDelay", 1f / preAttackDelay);
//            animator.SetBool("PreAttack", true);

            
//        }
//        state = NodeState.RUNNING;
//        return state;
//    }

//    public virtual void FinishedPreAttackAnimEvent()
//    {
//        animator.SetBool("Attacking", true);
//        agent.isStopped = true;

//        Projectile projectile = GameObject.Instantiate<Projectile>(projectilePrefab, projectileSpawnPos.position, Quaternion.identity);
//        Vector3 TargetPos = entity.TargetSpot.position;
//        Vector3 shootDir = (TargetPos - projectileSpawnPos.position).normalized;
//        projectile.transform.LookAt(TargetPos);
//        projectile.Initialize(entity, shootDir);

//    }

//    public virtual void FinishedAttackAnimEvent()
//    {
//        entity.CurrentAttackDelay = attackDelay;
//        animator.SetBool("Attacking", false);
//        animator.SetBool("PreAttack", false);
//        agent.isStopped = false;
//    }
//}

