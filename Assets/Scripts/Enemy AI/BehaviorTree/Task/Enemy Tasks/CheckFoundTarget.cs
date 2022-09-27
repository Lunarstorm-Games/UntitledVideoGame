using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.EnemyTask
{
    public class CheckFoundTarget : Node
    {
        protected Enemy enemy;
        public CheckFoundTarget(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public override NodeState Evaluate()
        {
            
            if (enemy.CurrentTarget == null || !enemy.CurrentTarget.gameObject.activeInHierarchy)
            {
                Entity[] targetInterests = GameObject.FindObjectsOfType<Entity>();
                if (targetInterests.Length > 0)
                {
                    Entity closest = null;
                    float distance = Mathf.Infinity;

                    foreach (Entity target in targetInterests)
                    {
                        if (target.Type == TargetType.Enemy)
                            continue;

                        Vector3 diff = target.transform.position - enemy.transform.position;
                        float curDistance = diff.sqrMagnitude;
                        if (curDistance < distance)
                        {
                            closest = target;
                            distance = curDistance;
                        }
                    }
                    enemy.CurrentTarget = closest;
                    state = NodeState.SUCCESS;
                    return state;

                }
                state = NodeState.FAILURE;
                return state;
            }
            state = NodeState.SUCCESS;
            return state;
        }

    } 
}
