using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    protected float currentAttackDelay;

    public AttackState(Enemy entity, FiniteStateMachine fsm, EnemyData entityData) : base(entity, fsm, entityData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        currentAttackDelay = 0f;
        entity.Animator.SetFloat("Speed", 0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        entity.transform.LookAt(entity.CurrentTarget.transform);
        entity.Agent.SetDestination(entity.CurrentTarget.transform.position);

        currentAttackDelay -= Time.deltaTime;
        if (currentAttackDelay <= 0f)
        {
            currentAttackDelay = entityData.attackDelay;
            entity.Animator.SetTrigger("Attacking");
        }

        float distance = Vector3.Distance(entity.transform.position, entity.CurrentTarget.transform.position);
        if (distance > entityData.attackRange)
        {
            fsm.ChangeState(entity.ChaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
