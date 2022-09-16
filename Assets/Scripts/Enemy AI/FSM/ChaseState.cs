using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyState
{
    public ChaseState(Enemy entity, FiniteStateMachine fsm, EnemyData entityData) : base(entity, fsm, entityData)
    {
    }

    public override void Enter()
    {
        base.Enter();

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
        entity.Animator.SetFloat("Speed", entity.Agent.velocity.magnitude / entity.Agent.speed);

        float distance = Vector3.Distance(entity.transform.position, entity.CurrentTarget.transform.position);
        if (distance < entityData.attackRange)
        {
            fsm.ChangeState(entity.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
