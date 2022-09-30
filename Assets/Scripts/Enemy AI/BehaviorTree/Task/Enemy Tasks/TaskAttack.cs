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
        protected float attackDelay;
        protected float preAttackDelay;
        protected Enemy enemy;
        protected float damage;
        protected MeleeWeapon weapon;

        public TaskAttack(MeleeEnemy enemy)
        {
            this.animator = enemy.Animator;
            this.agent = enemy.Agent;
            this.attackDelay = enemy.AttackDelay;
            this.preAttackDelay = enemy.PreAttackDelay;
            this.damage = enemy.Damage;
            this.enemy = enemy;
            this.weapon = enemy.Weapon;

            this.enemy.OnPreAttackFinish.AddListener(FinishedPreAttackAnimEvent);
            this.enemy.OnAttackFinish.AddListener(FinishedAttackAnimEvent);
        }

        public override NodeState Evaluate()
        {
            Vector3 dir = enemy.CurrentTarget.transform.position - enemy.transform.position;
            dir.y = 0;
            enemy.transform.rotation = Quaternion.LookRotation(dir);

            animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed * Time.deltaTime);


            enemy.CurrentAttackDelay -= Time.deltaTime;
            if (enemy.CurrentAttackDelay <= 0f && !animator.GetBool("PreAttack"))
            {
                animator.SetFloat("PreAttackDelay", 1f / preAttackDelay);
                animator.SetBool("PreAttack", true);
            }

            state = NodeState.RUNNING;
            return state;
        }

        public virtual void FinishedPreAttackAnimEvent()
        {
            weapon.Initialize(enemy);
            animator.SetBool("Attacking", true);
            agent.isStopped = true;

        }

        public virtual void FinishedAttackAnimEvent()
        {
            enemy.CurrentAttackDelay = attackDelay;
            animator.SetBool("Attacking", false);
            animator.SetBool("PreAttack", false);
            agent.isStopped = false;
        }
    } 
}
