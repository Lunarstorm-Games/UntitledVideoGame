using BehaviorTree;
using BehaviorTree.EnemyTask;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class SkeletonMageTree : Tree
    {
        protected SkeletonMage enemy;
        public SkeletonMageTree(SkeletonMage enemy)
        {
            this.enemy = enemy;
        }

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckHitByPlayer(enemy),
                    new TaskGoToTarget(enemy.Animator, enemy.Agent, enemy),
                }),
                new Sequence(new List<Node>
                {
                    new CheckTargetInAttackRange(enemy),
                    new TaskRangeAttack(enemy.Animator, enemy.Agent, enemy.PreAttackDelay, enemy.AttackDelay, enemy.Damage, enemy.projectileRange, enemy.projectileSpeed, enemy, enemy.projectilePrefab, enemy.projectileSpawnPos),
                }),
                new Sequence(new List<Node>
                {
                    new CheckPlayerInAggroRange(enemy.PlayerPos, enemy, enemy.AggroRange),
                    new TaskGoToTarget(enemy.Animator, enemy.Agent, enemy),
                }),
                new Sequence(new List<Node>
                {
                    new CheckFoundTarget(enemy),
                    new TaskGoToTarget(enemy.Animator, enemy.Agent, enemy),
                }),

            });  

            return root;
        }
    }
}
