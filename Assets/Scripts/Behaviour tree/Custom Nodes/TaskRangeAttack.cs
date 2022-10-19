using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskRangeAttack : Node
{
    protected Animator animator;
    protected NavMeshAgent agent;
    protected float currentAttackDelay;
    protected float preAttackDelay;
    protected float attackDelay;
    protected float damage;
    protected float projectileRange;
    protected float projectileSpeed;
    protected EntityAI entity;
    protected Projectile projectilePrefab;
    protected Transform projectileSpawnPos;

    public TaskRangeAttack(EntityAI entity, Projectile projectile, Transform projectileSpawnPos)
    {
        this.animator = entity.Animator;
        this.agent = entity.Agent;
        this.currentAttackDelay = entity.AttackDelay;
        this.attackDelay = entity.AttackDelay;
        this.entity = entity;
        this.projectilePrefab = projectile;
        this.projectileSpawnPos = projectileSpawnPos;
    }

    public override NodeState Evaluate()
    {
        Vector3 dir = entity.CurrentTarget.transform.position - entity.transform.position;
        dir.y = 0;
        entity.transform.rotation = Quaternion.LookRotation(dir);

        animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

        currentAttackDelay -= Time.deltaTime;
        if (currentAttackDelay <= 0f)
        {
            currentAttackDelay = attackDelay;
            animator.SetTrigger("Attacking");

            Projectile projectile = GameObject.Instantiate<Projectile>(projectilePrefab, projectileSpawnPos.position, Quaternion.identity);
            Vector3 TargetOffset = entity.CurrentTarget.GetTargetOffset();
            Vector3 shootDir = (TargetOffset - projectileSpawnPos.position).normalized;
            projectile.transform.LookAt(TargetOffset);
            projectile.Initialize(entity, shootDir);
        }
        state = NodeState.RUNNING;
        return state;
    }
}

