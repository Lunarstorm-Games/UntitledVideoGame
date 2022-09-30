using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree.EnemyTask
{
    public class CheckTargetInAttackRange : Node
    {
        protected Enemy enemy;
        protected NavMeshAgent agent;
        protected Animator animator;
        protected float attackRange;

        public CheckTargetInAttackRange(Enemy enemy)
        {
            this.attackRange = enemy.AttackRange;
            this.agent = enemy.Agent;
            this.animator = enemy.Animator;
            this.enemy = enemy;
        }

        public override NodeState Evaluate()
        {
            Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, attackRange);
            foreach (Collider collider in hitColliders)
            {
                if (collider.TryGetComponent<Entity>(out Entity entity))
                {
                    if (entity == enemy.CurrentTarget)
                    {
                        agent.isStopped = true;
                        state = NodeState.SUCCESS;
                        return state;
                    }
                }
            }

            enemy.CurrentAttackDelay = 0f;
            animator.SetBool("PreAttack", false);
            state = NodeState.FAILURE;
            return state;
        }

    } 
}
