//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AttackState : EnemyState
//{

//    public AttackState(Enemy enemy, FiniteStateMachine fsm, EnemyData enemyData) : base(enemy, fsm, enemyData)
//    {
//    }

//    public override void Enter()
//    {
//        base.Enter();
//        enemy.Animator.SetFloat("Speed", 0f);
//        enemyData.currentAttackDelay = enemyData.preAttackDelay;
//    }

//    public override void Exit()
//    {
//        base.Exit();
//    }

//    public override void LogicUpdate()
//    {
//        base.LogicUpdate();

//        enemy.LookAtTarget();

//        enemy.Attack();

//        if (!enemy.InAttackRange())
//        {
//            fsm.ChangeState(enemy.FollowTargetState);
//        }
//    }

//    public override void PhysicsUpdate()
//    {
//        base.PhysicsUpdate();
//    }
//}
