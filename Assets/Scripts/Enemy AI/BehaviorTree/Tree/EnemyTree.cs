using BehaviorTree.EnemyTask;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class EnemyTree : Tree
    {
        protected Enemy enemy;

        public EnemyTree(Enemy enemy)
        {
            this.enemy = enemy;
        }

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                
                new Sequence(new List<Node>
                {
                    new CheckTargetInAttackRange(enemy),
                    new TaskAttack(enemy),
                }),
                new Sequence(new List<Node>
                {
                    new CheckPlayerInAggroRange(enemy, enemy.PlayerPos),
                    new TaskGoToTarget(enemy),
                }),
                new Sequence(new List<Node>
                {
                    new CheckFoundTarget(enemy),
                    new TaskGoToTarget(enemy),
                }),
                
            });

            return root;
        }
    }
}
