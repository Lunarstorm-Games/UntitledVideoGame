using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNewTargetState : EnemyState
{
    public FindNewTargetState(Enemy enemy, FiniteStateMachine fsm, EnemyData enemyData) : base(enemy, fsm, enemyData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.FindClosestTarget();
        fsm.ChangeState(enemy.FollowTargetState);
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
