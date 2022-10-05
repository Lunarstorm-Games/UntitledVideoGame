using BehaviorTree.EnemyTask;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    public class MeleeSkeletonTree : BehaviourTree
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
                new CheckDeath(enemy),
                new Sequence(new List<Node>
                {
                    new CheckHitByPlayer(enemy),
                    new TaskGoToTarget(enemy),
                }),
                new Sequence(new List<Node>
                {
                    new CheckTargetInAttackRange(enemy),
                    new TaskAttack(enemy),
                }),
                new Sequence(new List<Node>
                {
                    new CheckTargetIsPlayer(enemy),
                    new TaskGoToTarget(enemy),
                }),
                new Sequence(new List<Node>
                {
                    new CheckTargetInAggroRange(enemy),
                    new TaskGoToTarget(enemy),
                }),
                new GoToTreeTarget(enemy),
            });

            return root;
        }
    }
}
