using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header("Weapon Component")]
    [SerializeField] public MeleeWeapon Weapon;
}
