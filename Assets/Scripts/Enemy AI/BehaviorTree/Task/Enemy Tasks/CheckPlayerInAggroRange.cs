
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.EnemyTask
{
    public class CheckPlayerInAggroRange : Node
    {
        protected Enemy enemy;
        protected Transform player;

        public CheckPlayerInAggroRange(Enemy enemy, Transform player)
        {
            this.enemy = enemy;
            this.player = player;
        }

        public override NodeState Evaluate()
        {
            if (enemy.CurrentTarget == null || !enemy.CurrentTarget.activeInHierarchy)
            {
                state = NodeState.FAILURE;
                return state;
            }

            if (Vector3.Distance(enemy.transform.position, player.position) <= enemy.aggroRange)
            {
                enemy.CurrentTarget = player.gameObject;
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    } 
}
