
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich : RangeEnemy
{
    [Header("Entity specific fields")]
    [SerializeField] private float summonCooldown;
    [SerializeField] private bool canSummon;
    [SerializeField] private List<Entity> minions; 

    private float summonCooldownTime;

    public float SummonCooldown { get => summonCooldown; set => summonCooldown = value; }
    public bool CanSummon { get => canSummon; set => canSummon = value; }
    public List<Entity> Minions { get => minions; set => minions = value; }

    public override void Awake()
    {
        base.Awake();
        summonCooldownTime = summonCooldown;
    }

    public void Update()
    {
        UpdateSummonCooldown();
        
    }

    private void UpdateSummonCooldown()
    {
        if (summonCooldownTime < 0f)
        {
            summonCooldownTime = summonCooldown;
            canSummon = true;
        }
        else
        {
            canSummon = false;
            summonCooldownTime -= Time.deltaTime;
        }
    }
}
