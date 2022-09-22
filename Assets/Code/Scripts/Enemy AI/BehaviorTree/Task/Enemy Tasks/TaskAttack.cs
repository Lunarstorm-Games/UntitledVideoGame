using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.EnemyTask
{
    public class TaskAttack : Node
    {
        protected Enemy enemy;
        protected float currentAttackDelay = 0f;

        public TaskAttack(Enemy enemy)
        {
            this.enemy = enemy;
            this.currentAttackDelay = enemy.preAttackDelay;
        }

        public override NodeState Evaluate()
        {
            Vector3 dir = enemy.CurrentTarget.transform.position - enemy.transform.position;
            dir.y = 0;
            enemy.transform.rotation = Quaternion.LookRotation(dir);

            enemy.Animator.SetFloat("Speed", enemy.Agent.velocity.magnitude / enemy.Agent.speed);

            currentAttackDelay -= Time.deltaTime;
            if (currentAttackDelay <= 0f)
            {
                currentAttackDelay = enemy.attackDelay;
                enemy.Animator.SetTrigger("Attacking");

                if (enemy.CurrentTarget.TryGetComponent<IDamageable>(out IDamageable target))
                {
                    target.TakeDamage(enemy.damage, enemy);
                }
            }
            state = NodeState.RUNNING;
            return state;
        }
    } 
}
