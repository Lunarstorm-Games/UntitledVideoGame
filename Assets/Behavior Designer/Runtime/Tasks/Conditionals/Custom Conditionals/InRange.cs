using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class InRange : Conditional
{
    public SharedEntity firstEntity;
    public SharedEntity secondEntity;
    public SharedFloat range;

    public override TaskStatus OnUpdate()
    {
        if (firstEntity == null) return TaskStatus.Failure;

        if (secondEntity == null) return TaskStatus.Failure;

        if (Vector3.Distance(firstEntity.Value.transform.position, secondEntity.Value.transform.position) > range.Value)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }
}