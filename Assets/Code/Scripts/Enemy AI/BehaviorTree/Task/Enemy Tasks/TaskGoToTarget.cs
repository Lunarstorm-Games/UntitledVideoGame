using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.EnemyTask
{
    public class TaskGoToTarget : Node
    {
        protected Enemy enemy;

        public TaskGoToTarget(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public override NodeState Evaluate()
        {
            Vector3 dir = enemy.CurrentTarget.transform.position - enemy.transform.position;
            dir.y = 0;
            enemy.transform.rotation = Quaternion.LookRotation(dir);

            enemy.Agent.SetDestination(enemy.CurrentTarget.transform.position);

            enemy.Animator.SetFloat("Speed", enemy.Agent.velocity.magnitude / enemy.Agent.speed);

            state = NodeState.RUNNING;
            return state;
        }

    } 
}
