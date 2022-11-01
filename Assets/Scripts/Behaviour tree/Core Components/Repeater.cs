using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater : Node
{
    public Node RunningNode;
    public Repeater() : base() { }

    public Repeater(Node childNode) : base(childNode)
    {
    }

    public Repeater(List<Node> children) : base(children)
    {
    }

    public override NodeState Evaluate()
    {
        if (RunningNode != null)
        {
            switch (RunningNode.Evaluate())
            {
                case NodeState.FAILURE:
                    state = NodeState.FAILURE;
                    return state;
                case NodeState.SUCCESS:
                    RunningNode = null;
                    state = NodeState.SUCCESS;
                    return state;
                case NodeState.RUNNING:
                    state = NodeState.RUNNING;
                    return state;
            }
        }

        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    continue;
                case NodeState.SUCCESS:
                    state = NodeState.SUCCESS;
                    return state;
                case NodeState.RUNNING:
                    RunningNode = node;
                    state = NodeState.RUNNING;
                    return state;
                default:
                    continue;
            }
        }

        state = NodeState.FAILURE;
        return state;
    }
}
