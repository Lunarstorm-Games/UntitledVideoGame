using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WizardBossLightningProjectile : Projectile
{
    [SerializeField] private Collider thunderColl;
    [SerializeField] private VisualEffect thunderEffect;
    [SerializeField] private Transform AOE_Center;

    public float AOE_Damage;
    public float AOE_Time;
    public float ThunderAOETickRate;
    public float ThunderAOERadius;
    private float thunderStrikeTime;
    private float thunderAOETickRateTime;

    public Collider ThunderColl { get => thunderColl; }
    public VisualEffect ThunderEffect { get => thunderEffect;}


    public override void Start()
    {
        base.Start();
        thunderStrikeTime = ThunderEffect.GetFloat("ThunderAOEDelay");
    }

    public override void Update()
    {
        if (thunderStrikeTime < 0)
        {
            ThunderColl.enabled = false;
            if (AOE_Time > 0)
            {
                OnAOETriggerStay();
                AOE_Time -= Time.deltaTime;
            }
            else DestroyProjectile();
        }
        else thunderStrikeTime -= Time.deltaTime;

    }

    public override void OnTriggerEnter(Collider collider)
    {
        DealDamage(collider, damage);
    }

    public void OnAOETriggerStay()
    {
        if (thunderAOETickRateTime < 0)
        {
            Collider[] AOETargets = Physics.OverlapSphere(AOE_Center.position, ThunderAOERadius);
            {
                foreach (Collider hitTarget in AOETargets)
                {
                    DealDamage(hitTarget, AOE_Damage);
                }
            }
            thunderAOETickRateTime = ThunderAOETickRate;
        }
        else thunderAOETickRateTime -= Time.deltaTime;
        
    }


    public void DealDamage(Collider collider, float damage)
    {
        if (collider.transform.root.TryGetComponent<Entity>(out Entity entity))
        {
            if (entity != shooter)
            {
                if (shooter.IsValidTarget(entity.EntityType))
                {
                    if (collider.transform.root.TryGetComponent<IDamageable>(out IDamageable target))
                    {
                        target.TakeDamage(damage, shooter);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AOE_Center.position, ThunderAOERadius);
    }


}
