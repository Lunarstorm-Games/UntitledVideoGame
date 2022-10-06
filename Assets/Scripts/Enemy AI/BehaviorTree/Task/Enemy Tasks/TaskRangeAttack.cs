using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree.EnemyTask
{
    public class TaskRangeAttack : Node
    {
        protected Animator animator;
        protected NavMeshAgent agent;
        protected float currentAttackDelay;
        protected float preAttackDelay;
        protected float attackDelay;
        protected float damage;
        protected float projectileRange;
        protected float projectileSpeed;
        protected RangeEnemy enemy;
        protected Projectile projectilePrefab;
        protected Transform projectileSpawnPos;

        public TaskRangeAttack(RangeEnemy enemy)
        {
            this.animator = enemy.Animator;
            this.agent = enemy.Agent;
            this.currentAttackDelay = enemy.AttackDelay;
            this.attackDelay = enemy.AttackDelay;
            this.enemy = enemy;
            this.projectilePrefab = enemy.Projectile;
            this.projectileSpawnPos = enemy.ProjectileSpawnPos;
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

                Projectile projectile = GameObject.Instantiate<Projectile>(projectilePrefab, projectileSpawnPos.position, Quaternion.identity);
                Vector3 TargetOffset = enemy.CurrentTarget.SetTargetOffset();
                Vector3 shootDir = (TargetOffset - projectileSpawnPos.position).normalized;
                projectile.transform.LookAt(TargetOffset);
                projectile.Initialize(enemy, shootDir);
            }
            state = NodeState.RUNNING;
            return state;
        }
    } 
}
