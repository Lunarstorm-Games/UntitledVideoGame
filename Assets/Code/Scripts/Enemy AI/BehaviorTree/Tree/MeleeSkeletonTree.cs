using BehaviorTree.EnemyTask;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    public class MeleeSkeletonTree : Tree
    {
        protected MeleeSkeleton enemy;

        public MeleeSkeletonTree(MeleeSkeleton enemy)
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
                    new TaskAttack(enemy.Animator, enemy.Agent, enemy.AttackDelay, enemy.PreAttackDelay, enemy.Damage, enemy),
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
