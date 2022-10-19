using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckTargetIsPlayer : Node
{
    protected Enemy enemy;

    public CheckTargetIsPlayer(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        if (enemy.CurrentTarget != null && enemy.CurrentTarget == Player.Instance)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;

    }
}

