using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnState : EnemyState
{
    public SpawnState(Enemy entity, FiniteStateMachine fsm, EnemyData entityData) : base(entity, fsm, entityData)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //Find Closest target
        Target closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = entity.transform.position;
        foreach (Target go in entity.TargetInterests)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        entity.CurrentTarget = closest.gameObject;
        entity.Agent.speed = entityData.speed;
        entity.Agent.stoppingDistance = entityData.attackRange;
        fsm.ChangeState(entity.ChaseState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
