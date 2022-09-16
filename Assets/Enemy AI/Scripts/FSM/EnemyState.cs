using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : BaseState
{
    protected EnemyData entityData;
    protected FiniteStateMachine fsm;
    protected Enemy entity;

    public EnemyState(Enemy entity, FiniteStateMachine fsm, EnemyData entityData)
    {
        this.entity = entity;
        this.entityData = entityData;
        this.fsm = fsm;
    }

    public override void Enter()
    {
        startTime = Time.time;
    }

    public override void Exit()
    {

    }

    public override void LogicUpdate()
    {

    }

    public override void PhysicsUpdate()
    {

    }
}
