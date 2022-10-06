using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EntityType
{
    Player,
    NPC,
    Enemy,
    Structure,
}

[System.Flags]
public enum TargetInterests
{
    Player = 1,
    NPC = 2,
    Enemy = 4,
    Structure = 8,
}

public class Entity : MonoBehaviour
{
    public EntityType Type;
    public TargetInterests TargetsType;
    [HideInInspector] private Vector3 Offset;
    [SerializeField] private Vector3 TargetOffset = Vector3.zero;

    public Vector3 SetTargetOffset()
    {
        Offset = transform.position;
        Offset += TargetOffset;
        return Offset;
    }

    public bool ValidTarget(TargetInterests targetInterests, EntityType target)
    {
        return targetInterests.ToString().Contains(target.ToString());
    }
}

