using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeVincent
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
                new BossNodeTest(boss),
                
            });

            return root;
        }
    }
}
