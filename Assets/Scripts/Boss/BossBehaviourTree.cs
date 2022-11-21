using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BossBehaviourTree : BehaviourTree
    {
        protected Boss boss;

        public BossBehaviourTree(Boss boss)
        {
            this.boss = boss;
        }

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new CheckDeath(boss),
                
            });

            return root;
        }
    }
}
