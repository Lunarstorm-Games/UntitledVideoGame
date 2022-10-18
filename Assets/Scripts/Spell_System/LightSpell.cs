// using System;
// using UnityEngine;
// using UnityEngine.VFX;
//
// public class LightSpell : SpellBase
// {
//     public float CurrentDamage;
//     public float CurrentSpeed;
//     public float DamageGrowthAmount;
//     public float SpeedGrowthAmount;
//     
//     [Header("Info Only Attributes")]
//     public int DamageLevel = 1;
//     public int SpeedLevel = 1;
//
//     void Awake()
//     {
//         CurrentDamage = StartDamage;
//         CurrentSpeed = SpellSpeed;
//     }
//
//     public void UpgradeDamage()
//     {
//         DamageLevel += 1;
//         CurrentDamage += DamageGrowthAmount;
//     }
//
//     public void UpgradeProjectileSpeed()
//     {
//         SpeedLevel += 1;
//         CurrentSpeed += SpeedGrowthAmount;
//     }
// }