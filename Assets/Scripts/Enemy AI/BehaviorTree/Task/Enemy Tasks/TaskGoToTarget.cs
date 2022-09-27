using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree.EnemyTask
{
    public class TaskGoToTarget : Node
    {
        protected Animator animator;
        protected NavMeshAgent agent;
        protected Enemy enemy;

        public TaskGoToTarget(Enemy enemy)
        {
            this.animator = enemy.Animator;
            this.agent = enemy.Agent;
            this.enemy = enemy;
        }

        public override NodeState Evaluate()
        {
            //Vector3 dir = target.transform.position - entity.transform.position;
            //dir.y = 0;
            //entity.transform.rotation = Quaternion.LookRotation(dir);

            agent.SetDestination(enemy.CurrentTarget.transform.position);

            animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

            state = NodeState.RUNNING;
            return state;
        }

    } 
}
