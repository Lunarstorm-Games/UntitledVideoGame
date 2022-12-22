using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class TeleportNearPlayer : Action
{
	[SerializeField] private SharedFloat warpRange;
	[SerializeField] private SharedEntity target;
	private Animator animator;
	private NavMeshAgent agent;

	public override void OnStart()
	{
		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	}

	public override TaskStatus OnUpdate()
	{
		Vector3 warpPoint = RandomPointOnCircleEdge(warpRange.Value);
		warpPoint += target.Value.transform.position;

		if (agent != null && agent.Warp(warpPoint))
			return TaskStatus.Success;

		return TaskStatus.Failure;
	}

	private Vector3 RandomPointOnCircleEdge(float radius)
	{
		var vector2 = Random.insideUnitCircle.normalized * radius;
		return new Vector3(vector2.x, 0, vector2.y);
	}
}