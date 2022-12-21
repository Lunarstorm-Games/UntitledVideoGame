using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entities.AI;
using UnityEngine;
using UnityEngine.AI;

public class WalkToMainObjective : Action
{
    [SerializeField] private SharedFloat speed;
    [SerializeField] private SharedFloat stoppingDistance;
    [SerializeField] private SharedTransform targetSpot;
    [SerializeField] private SharedEntity target;
    [SerializeField] private SharedGameObject waypoint;

    private Entity mainObjective;
    private NavMeshAgent agent;
    private Entity unit;
    private Animator animator;
    private WaypointNode targetWaypoint;

    public override void OnStart()
    {
        unit = GetComponent<Entity>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (!agent) return;


        if (waypoint.Value == null)
        {
            waypoint.SetValue(WaypointTree.Instance?.GetClosestStartingPoint(unit.transform).gameObject);
            targetWaypoint = waypoint?.Value.GetComponent<WaypointNode>();
           
        }
        else if(waypoint.Value !=null)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                targetWaypoint = targetWaypoint.NextNode();
            }
        }

        if (targetWaypoint == null)
        {
            mainObjective = GameObject.FindGameObjectWithTag("MainObjective").GetComponent<Entity>();
            if (mainObjective == null) return;
            if (!unit.IsValidTarget(mainObjective.EntityType)) return;
            target.SetValue(mainObjective);
            targetSpot.SetValue(mainObjective.GetEntityTargetSpot());
            agent.SetDestination(targetSpot.Value.position);
        }
        else
        {
            if (targetWaypoint==null) return;
            target.SetValue(targetWaypoint.GetComponent<Entity>());
            targetSpot.SetValue(targetWaypoint.transform);
            agent.SetDestination(targetSpot.Value.position);
        }


        agent.isStopped = false;
        agent.speed = speed.Value;
        agent.stoppingDistance = stoppingDistance.Value;



        animator.SetFloat("Speed", 1f);
    }

    public override void OnEnd()
    {
        if (agent)
            agent.isStopped = true;
    }


    public override TaskStatus OnUpdate()
    {
        if (!agent)
        {
            Debug.LogWarning("NavAgent is null");
            return TaskStatus.Failure;
        }

        if (mainObjective == null && targetWaypoint==null)
        {
            Debug.LogWarning("Target is null");
            return TaskStatus.Failure;
        }

        if (!agent.pathPending)
            if (!agent.isOnOffMeshLink)
                if (agent.remainingDistance <= agent.stoppingDistance)
                    return TaskStatus.Success;

        return TaskStatus.Running;
    }
}