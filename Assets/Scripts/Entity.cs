using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum EntityType
    {
        Player,
        NPC,
        Enemy,
    }

    public EntityType type;
}
