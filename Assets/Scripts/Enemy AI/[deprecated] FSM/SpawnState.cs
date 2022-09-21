//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SpawnState : EnemyState
//{
//    public SpawnState(Enemy enemy, FiniteStateMachine fsm, EnemyData enemyData) : base(enemy, fsm, enemyData)
//    {
//    }

//    public override void Enter()
//    {
//        base.Enter();

//        enemy.Agent.speed = enemyData.speed;
//        enemy.Agent.stoppingDistance = enemyData.attackRange * 0.85f;
//        enemyData.currentHealth = enemyData.health;
//        fsm.ChangeState(enemy.FindNewTargetState);
//    }

//    public override void Exit()
//    {
//        base.Exit();
//    }

//    public override void LogicUpdate()
//    {
//        base.LogicUpdate();
//    }

//    public override void PhysicsUpdate()
//    {
//        base.PhysicsUpdate();
//    }
//}
