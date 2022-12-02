using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HitByPlayer : Conditional
{
	[SerializeField] private SharedEntity target;
    [SerializeField] private SharedEntity targetPlayer;

	public override TaskStatus OnUpdate()
	{
		if (targetPlayer.Value != null && targetPlayer.Value == Player.Instance)
		{
			target.SetValue(targetPlayer.Value);
			//targetPlayer.SetValue(null);

			return TaskStatus.Success;

		}
		return TaskStatus.Failure;

	}
}