using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElemental : Enemy
{
    [Header("Earth Elemental specific stats")]
    [SerializeField] protected GameObject healAura;
    [SerializeField] protected float healEffectRange = 7f;
    [SerializeField] protected float healing = 5f;
    [SerializeField] protected float healingDelay = 2f;

    protected float currentHealingDelay;

    public override void OnEnable()
    {
        base.OnEnable();
        healAura.transform.localScale = new Vector3(healEffectRange * 2, 1f, healEffectRange * 2);
    }

    public void Update()
    {
        HealNearbyAlly();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healEffectRange);
    }

    private void HealNearbyAlly()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, healEffectRange);

        if (currentHealingDelay <= 0f)
        {
            currentHealingDelay = healingDelay;
            foreach (Collider collider in colliders)
            {
                if (collider.transform.root.TryGetComponent<Entity>(out Entity entity))
                {
                    if (!IsValidTarget(entity.EntityType))
                    {
                        entity.CurrentHealth += healing;
                    }
                }
            }
        }
        else currentHealingDelay -= Time.deltaTime;

        
    }
}
