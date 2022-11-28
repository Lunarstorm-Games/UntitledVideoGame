using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ValidTarget : Conditional
{
	public override TaskStatus OnUpdate()
	{
		return TargetsType.ToString().Contains(target.ToString());
		return TaskStatus.Success;
	}

	public bool ValidTarget(EntityType type)
    {
		return TargetsType.ToString().Contains(type.ToString());
	}
}