using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.EnemyTask
{
    public class CheckHitByPlayer : Node
    {
        protected Enemy enemy;

        public CheckHitByPlayer(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public override NodeState Evaluate()
        {
            if (enemy.HitByTarget != null && enemy.HitByTarget.CompareTag("Player"))
            {
                enemy.CurrentTarget = enemy.HitByTarget;
                enemy.HitByTarget = null;
                state = NodeState.SUCCESS;
                return state;
                
            }
            state = NodeState.FAILURE;
            return state;

        }
    }
}
