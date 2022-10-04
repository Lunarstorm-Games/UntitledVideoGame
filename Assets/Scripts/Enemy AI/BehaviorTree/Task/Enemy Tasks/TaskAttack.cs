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
        protected Enemy enemy;
        protected float damage;

        public TaskAttack(MeleeEnemy enemy)
        {
            this.animator = enemy.Animator;
            this.agent = enemy.Agent;
            this.currentAttackDelay = enemy.PreAttackDelay;
            this.attackDelay = enemy.AttackDelay;
            this.preAttackDelay = enemy.PreAttackDelay;
            this.damage = enemy.Damage;
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
