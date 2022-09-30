
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.EnemyTask
{
    public class CheckTargetInAggroRange : Node
    {
        protected Enemy enemy;
        protected float aggroRange;

        public CheckTargetInAggroRange(Enemy enemy)
        {
            this.enemy = enemy;
            this.aggroRange = enemy.AggroRange;
        }

        public override NodeState Evaluate()
        {
            Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, aggroRange);
            foreach (Collider collider in hitColliders)
            {
                if (collider.TryGetComponent<Entity>(out Entity target))
                {
                    if (enemy.ValidTarget(enemy.TargetsType, target.Type))
                    {
                        enemy.CurrentTarget = target;
                        state = NodeState.SUCCESS;
                        return state;
                    }
                }
            }
            state = NodeState.FAILURE;
            return state;
        }
    } 
}
