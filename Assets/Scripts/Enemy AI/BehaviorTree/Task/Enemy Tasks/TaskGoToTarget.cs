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
            if (!animator.GetBool("Attacking"))
                agent.isStopped = false;

            agent.SetDestination(enemy.CurrentTarget.transform.position);

            animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

            state = NodeState.RUNNING;
            return state;
        }
    } 
}
