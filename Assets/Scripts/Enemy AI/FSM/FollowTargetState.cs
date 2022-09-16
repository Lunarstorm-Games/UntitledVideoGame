using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetState : EnemyState
{
    public FollowTargetState(Enemy enemy, FiniteStateMachine fsm, EnemyData enemyData) : base(enemy, fsm, enemyData)
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
        Vector3 dir = enemy.CurrentTarget.transform.position - enemy.transform.position;
        dir.y = 0;
        enemy.transform.rotation = Quaternion.LookRotation(dir);

        enemy.SetTargetDestination(enemy.CurrentTarget.transform.position);
        enemy.Animator.SetFloat("Speed", enemy.Agent.velocity.magnitude / enemy.Agent.speed);

        if (enemy.InAttackRange())
        {
            fsm.ChangeState(enemy.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
