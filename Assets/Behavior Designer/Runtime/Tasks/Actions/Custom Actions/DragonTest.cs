using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class DragonTest : Action
{
	[SerializeField] private SharedEntity unit;
    [SerializeField] private SharedTransform targetspot; 
	[SerializeField] private SharedEntity target;
	

	private Transform spot;

	public override void OnStart()
	{
		//unit.Value.GetComponent<Boss>().CurrentTarget = Player.Instance;
		//target.SetValue(Player.Instance);
		//spot = Player.Instance.GetEntityTargetSpot();
		//unit.Value.GetComponent<Boss>().TargetSpot = spot;
		//targetspot.SetValue(spot);
	}

	public override TaskStatus OnUpdate()
	{


		return TaskStatus.Success;
	}
}