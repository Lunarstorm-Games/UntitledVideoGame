using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class TaskIdle : Node
    {
        protected Animator animator;
        protected NavMeshAgent agent;

        public TaskIdle(Guard npc)
        {
            //this.animator = npc.Animator;
            //this.agent = npc.Agent;
        }

        public override NodeState Evaluate()
        {
            //animator.SetFloat("Speed", 0f);


            state = NodeState.RUNNING;
            return state;
        }
    }

