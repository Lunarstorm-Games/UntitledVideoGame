using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TargetType
{
    Player,
    NPC,
    Enemy,
    Structure,
}

public class Entity : MonoBehaviour
{
    public TargetType Type;
    [HideInInspector] public Vector3 Offset;
    [SerializeField] private Vector3 TargetOffset = Vector3.zero;

    public virtual void Update()
    {
        Offset = transform.position;
        Offset += TargetOffset;
    }
}
