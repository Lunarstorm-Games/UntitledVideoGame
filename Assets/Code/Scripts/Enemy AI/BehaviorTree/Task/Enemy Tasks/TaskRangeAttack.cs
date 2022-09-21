using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.EnemyTask
{
    public class TaskRangeAttack : Node
    {
        protected RangeEnemy enemy;
        protected float currentAttackDelay = 0f;

        public TaskRangeAttack(RangeEnemy enemy)
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

                Projectile projectile = GameObject.Instantiate<Projectile>(enemy.projectilePrefab, enemy.projectileSpawnPos.position, Quaternion.identity);
                Vector3 shootDir = (enemy.CurrentTarget.transform.position - enemy.projectileSpawnPos.position).normalized;
                projectile.transform.LookAt(enemy.CurrentTarget.transform);
                projectile.Initialize(enemy, enemy.projectileSpeed, enemy.damage, shootDir, enemy.projectileRange);
            }
            state = NodeState.RUNNING;
            return state;
        }
    } 
}
