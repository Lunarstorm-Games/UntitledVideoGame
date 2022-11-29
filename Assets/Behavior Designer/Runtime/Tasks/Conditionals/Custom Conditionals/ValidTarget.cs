using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ValidTarget : Conditional
{
	[SerializeField] private SharedEntity _target;
    [SerializeField] private EntityType _targetInterests;

	public override void OnAwake()
	{
		_targetInterests = GetComponent<Entity>().TargetInterests;
	}

	public override TaskStatus OnUpdate()
	{
		if (IsValid(_target.Value.EntityType))
			return TaskStatus.Success;
		else
			return TaskStatus.Failure;
	}

	public bool IsValid(EntityType type)
	{
		return _targetInterests.ToString().Contains(type.ToString());
	}


}