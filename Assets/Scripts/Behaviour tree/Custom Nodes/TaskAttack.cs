using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class TaskAttack : Node
{
    protected Animator animator;
    protected NavMeshAgent agent;
    protected float attackDelay;
    protected float preAttackDelay;
    protected EntityAI entity;
    protected MeleeWeapon weapon;

    public TaskAttack(EntityAI entity, MeleeWeapon weapon)
    {
        this.animator = entity.Animator;
        this.agent = entity.Agent;
        this.attackDelay = entity.AttackDelay;
        this.preAttackDelay = entity.PreAttackDelay;
        this.entity = entity;
        this.weapon = weapon;

        this.entity.OnPreAttackFinish.AddListener(FinishedPreAttackAnimEvent);
        this.entity.OnAttackFinish.AddListener(FinishedAttackAnimEvent);
    }

    public override NodeState Evaluate()
    {
        Vector3 dir = entity.TargetSpot.position - entity.transform.position;
        dir.y = 0f;
        entity.transform.rotation = Quaternion.LookRotation(dir);

        animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed * Time.deltaTime);


        entity.CurrentAttackDelay -= Time.deltaTime;
        if (entity.CurrentAttackDelay <= 0f && !animator.GetBool("PreAttack"))
        {
            animator.SetFloat("PreAttackDelay", 1f / preAttackDelay);
            animator.SetBool("PreAttack", true);
        }

        state = NodeState.RUNNING;
        return state;
    }

    public virtual void FinishedPreAttackAnimEvent()
    {
        weapon.Initialize(entity);
        animator.SetBool("Attacking", true);
        agent.isStopped = true;

    }

    public virtual void FinishedAttackAnimEvent()
    {
        entity.CurrentAttackDelay = attackDelay;
        animator.SetBool("Attacking", false);
        animator.SetBool("PreAttack", false);
        agent.isStopped = false;
    }
}

