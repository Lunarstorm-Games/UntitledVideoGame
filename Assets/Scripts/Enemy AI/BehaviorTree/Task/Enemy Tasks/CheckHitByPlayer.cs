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
            if (enemy.DamagedByGO != null && enemy.DamagedByGO.CompareTag("Player"))
            {
                if (enemy.DamagedByGO.TryGetComponent<Target>(out Target target))
                {
                    enemy.CurrentTarget = target;
                    enemy.DamagedByGO = null;
                    state = NodeState.SUCCESS;
                    return state;
                }
                state = NodeState.FAILURE;
                return state;

            }
            state = NodeState.FAILURE;
            return state;


        }
    }
}
