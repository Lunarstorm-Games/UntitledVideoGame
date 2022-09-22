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
        protected Enemy enemy;
        protected Projectile projectilePrefab;
        protected Transform projectileSpawnPos;

        public TaskRangeAttack(Animator animator, NavMeshAgent agent, float preAttackDelay, float attackDelay, float damage, float projectileRange, float projectileSpeed, Enemy enemy, Projectile projectilePrefab, Transform projectileSpawnPos)
        {
            this.animator = animator;
            this.agent = agent;
            this.currentAttackDelay = preAttackDelay;
            this.preAttackDelay = preAttackDelay;
            this.attackDelay = attackDelay;
            this.damage = damage;
            this.projectileRange = projectileRange;
            this.projectileSpeed = projectileSpeed;
            this.enemy = enemy;
            this.projectilePrefab = projectilePrefab;
            this.projectileSpawnPos = projectileSpawnPos;
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
                Vector3 shootDir = (enemy.CurrentTarget.targetOffset - projectileSpawnPos.position).normalized;
                projectile.transform.LookAt(enemy.CurrentTarget.targetOffset);
                projectile.Initialize(enemy, projectileSpeed, damage, shootDir, projectileRange);
            }
            state = NodeState.RUNNING;
            return state;
        }
    } 
}
