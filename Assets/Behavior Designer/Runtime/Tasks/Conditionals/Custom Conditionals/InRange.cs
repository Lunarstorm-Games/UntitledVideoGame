using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class InRange : Conditional
{
    public SharedFloat playerRange;
    public SharedFloat compareTo;
    public SharedBool compareHigher;

    public override TaskStatus OnUpdate()
    {
        if (compareHigher.Value)
        {
            if (playerRange.Value > compareTo.Value)
            {
                return TaskStatus.Success;
            }
            else return TaskStatus.Failure;
        }
        else
        {
            if (playerRange.Value < compareTo.Value)
            {
                return TaskStatus.Success;
            }
            else return TaskStatus.Failure;
        }
    }
}