using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entities.AI;
using Unity.VisualScripting;
using UnityEngine;

public class WaypointTree : MonoBehaviour
{
    [Tooltip("The first nodes of the waypoint system. Enemies go to the closest one")]
    public List<WaypointNode> StartingNodes = new();

    private static WaypointTree instance;
    public static WaypointTree Instance
    {
        get => instance; private set => instance = value;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogWarning($"multiple of singleton instance {nameof(WaypointTree)}");
            Destroy(gameObject);
        }
    }

    public WaypointNode GetClosestStartingPoint(Transform position)
    {
        WaypointNode tMin = null;
            float minDist = Mathf.Infinity;
           
            foreach (WaypointNode t in StartingNodes)
            {
                float dist = Vector3.Distance(t.transform.position, position.position);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            return tMin;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDrawGizmos(){
        
    }
}


