using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.EnemyTask
{
    public class CheckTargetInAttackRange : Node
    {
        protected Enemy enemy;

        public CheckTargetInAttackRange(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public override NodeState Evaluate()
        {
            if (enemy.CurrentTarget == null || !enemy.CurrentTarget.gameObject.activeInHierarchy)
            {
                state = NodeState.FAILURE;
                return state;
            }

            if (Vector3.Distance(enemy.transform.position, enemy.CurrentTarget.transform.position) <= enemy.AttackRange)
            {
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

    } 
}
