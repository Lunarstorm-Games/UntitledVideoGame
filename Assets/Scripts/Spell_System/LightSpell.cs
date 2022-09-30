using System;
using UnityEngine;
using UnityEngine.VFX;

public class LightSpell : SpellBase
{
    public int Level = 1;
    public float CurrentDamage;
    public float CurrentSpeed;
    public float DamageGrowthAmount;
    public float SpeedGrowthAmount;

    void Awake()
    {
        CurrentDamage = StartDamage;
        CurrentSpeed = SpellSpeed;
    }

    public void UpgradeDamage()
    {
        CurrentDamage += DamageGrowthAmount;
    }

    public void UpgradeProjectileSpeed()
    {
        CurrentSpeed += SpeedGrowthAmount;
    }
}