using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree.EnemyTask
{
    public class CheckDeath : Node
    {
        protected Enemy enemy;
        protected NavMeshAgent agent;
        protected Animator animator;
        protected Collider collider;

        public CheckDeath(Enemy enemy)
        {
            this.enemy = enemy;
            this.agent = enemy.Agent;
            this.animator = enemy.Animator;
        }

        public override NodeState Evaluate()
        {
            if (enemy.Death)
            {
                agent.isStopped = true;
                animator.SetTrigger("Death");

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}
