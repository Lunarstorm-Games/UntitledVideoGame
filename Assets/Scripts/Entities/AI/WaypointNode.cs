using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Entities.AI
{
    public class WaypointNode : MonoBehaviour
    {
        [Tooltip("Add all the next nodes the enemy will go after reaching the current one.")]
        public List<WaypointNode> nextNodes = new();

        void Start()
        {
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
            foreach (var nextNode in nextNodes)
            {
                if (nextNode)
                {
                    Gizmos.DrawLine(transform.position, nextNode.transform.position);
                }
            }
        }

        public WaypointNode NextNode()
        {
            if (nextNodes.Count == 0) return null;
            var i = Random.Range(0, nextNodes.Count);
            return nextNodes[i];
        }
    }
}