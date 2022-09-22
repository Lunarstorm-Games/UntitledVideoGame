using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree.EnemyTask
{
    public class TaskAttack : Node
    {
        protected Animator animator;
        protected NavMeshAgent agent;
        protected float currentAttackDelay;
        protected float attackDelay;
        protected float preAttackDelay;
        protected float damage;
        protected Enemy enemy;

        public TaskAttack(Animator animator, NavMeshAgent agent, float attackDelay, float preAttackDelay, float damage, Enemy enemy)
        {
            this.animator = animator;
            this.agent = agent;
            this.currentAttackDelay = preAttackDelay;
            this.attackDelay = attackDelay;
            this.preAttackDelay = preAttackDelay;
            this.damage = damage;
            this.enemy = enemy;
        }

        public override NodeState Evaluate()
        {
            Vector3 dir = enemy.CurrentTarget.transform.position - enemy.transform.position;
            dir.y = 0;
            enemy.transform.rotation = Quaternion.LookRotation(dir);

            animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

            currentAttackDelay -= Time.deltaTime;
            if (currentAttackDelay <= 0f)
            {
                currentAttackDelay = attackDelay;
                animator.SetTrigger("Attacking");

                if (enemy.CurrentTarget.TryGetComponent<IDamageable>(out IDamageable target))
                {
                    target.TakeDamage(damage, enemy);
                }
            }
            state = NodeState.RUNNING;
            return state;
        }
    } 
}
