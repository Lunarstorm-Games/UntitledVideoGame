using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterElemental : RangeEnemy
{
    [Header("Water Elemental specific stats")]
    [SerializeField] protected WaterElementalShield shield;
    [SerializeField] protected float shieldHealth = 150f;
    [SerializeField] protected float shieldWidth = 5f;
    [SerializeField] protected float shieldHeight = 3f;

    protected float currentShieldHealth;

    public override void OnEnable()
    {
        base.OnEnable();
        currentShieldHealth = shieldHealth;
        shield.gameObject.SetActive(true);
        shield.SetHealth(currentShieldHealth);
        shield.gameObject.transform.localScale = new Vector3(shieldWidth, shieldHeight, 0.15f); 
    }
}
