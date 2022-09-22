
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.EnemyTask
{
    public class CheckPlayerInAggroRange : Node
    {
        protected Transform player;
        protected Enemy enemy;
        protected float aggroRange;

        public CheckPlayerInAggroRange(Transform player, Enemy enemy, float aggroRange)
        {
            this.player = player;
            this.enemy = enemy;
            this.aggroRange = aggroRange;
        }

        public override NodeState Evaluate()
        {
            if (player == null || !player.gameObject.activeInHierarchy)
            {
                state = NodeState.FAILURE;
                return state;
            }

            if (Vector3.Distance(enemy.transform.position, player.position) <= aggroRange)
            {
                enemy.CurrentTarget = player.GetComponent<Target>();
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    } 
}
