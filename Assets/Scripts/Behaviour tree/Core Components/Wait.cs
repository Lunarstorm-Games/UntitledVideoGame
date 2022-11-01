using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : Node
{
    private float waitTime;
    private Node node;
    private float currentWaitTime;

    public Wait(float duration, Node node)
    {
        this.waitTime = duration;
        this.node = node;
    }

    public override NodeState Evaluate()
    {
        currentWaitTime += Time.deltaTime;

        
        if (currentWaitTime >= waitTime)
        {
            //duration over
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    currentWaitTime = 0;
                    state = NodeState.FAILURE;
                    return state;
                case NodeState.SUCCESS:
                    currentWaitTime = 0;
                    state = NodeState.SUCCESS;
                    return state;
                case NodeState.RUNNING:
                    state = NodeState.RUNNING;
                    return state;
                default:
                    state = NodeState.SUCCESS;
                    return state;
            }
        }

        
        state = NodeState.FAILURE;
        return state;
    }


}
