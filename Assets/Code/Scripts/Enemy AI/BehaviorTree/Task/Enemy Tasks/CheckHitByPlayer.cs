using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.EnemyTask
{
    public class CheckHitByPlayer : Node
    {
        private Enemy enemy;

        public CheckHitByPlayer(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public override NodeState Evaluate()
        {
            if (enemy.HitByGO != null && enemy.HitByGO.CompareTag("Player"))
            {
                enemy.CurrentTarget = enemy.HitByGO;
                enemy.HitByGO = null;

                Debug.Log("hit by player");
                state = NodeState.SUCCESS;
                return state;
            }
            
            state = NodeState.FAILURE;
            return state;

        }
    }
}
