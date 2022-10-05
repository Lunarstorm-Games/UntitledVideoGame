
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class NPC_Warrior_Tree : BehaviourTree
    {
        protected NPC_Warrior npc;

        public NPC_Warrior_Tree(NPC_Warrior npc)
        {
            this.npc = npc;
        }

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                //new CheckDeath(npc),
                //new TaskIdle(npc),
                //new Sequence(new List<Node>
                //{
                    
                //}),
                
            });

            return root;
        }
    }

