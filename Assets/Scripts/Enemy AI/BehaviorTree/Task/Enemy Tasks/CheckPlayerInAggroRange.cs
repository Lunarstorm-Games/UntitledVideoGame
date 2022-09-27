
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.EnemyTask
{
    public class CheckPlayerInAggroRange : Node
    {
        protected Enemy enemy;
        protected float aggroRange;

        public CheckPlayerInAggroRange(Enemy enemy)
        {
            this.enemy = enemy;
            this.aggroRange = enemy.AggroRange;
        }

        public override NodeState Evaluate()
        {
            if (Player.Instance == null || !Player.Instance.gameObject.activeInHierarchy)
            {
                state = NodeState.FAILURE;
                return state;
            }

            if (Vector3.Distance(enemy.transform.position, Player.Instance.transform.position) <= aggroRange)
            {
                enemy.CurrentTarget = Player.Instance.GetComponent<Entity>();
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    } 
}
