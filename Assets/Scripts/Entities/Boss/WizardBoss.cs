using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBoss : MonoBehaviour
{
    [Header("Passive stats")]
    [SerializeField] protected float speed = 4f;
    [SerializeField] protected float stoppingDistance = 7f;

    [Header("Thunder Attack")]
    [SerializeField] protected float thunder_damage = 20f;
    [SerializeField] protected float thunder_speed = 7f;
    [Tooltip("The AOE from the thunder impact")]
    [SerializeField] protected float thunderAOEDamage = 1f;
    [Tooltip("How long the thunder AOE stays")]
    [SerializeField] protected float thunderAOETime = 4f;
    [Tooltip("Determines time between fireball shots")]
    [SerializeField] protected float thunder_attackSpeed = 1f;
    [SerializeField] protected float thunder_attackDelay = 10f;
    [SerializeField] protected Transform projectileSpawnPos;
    [SerializeField] protected Projectile projectile;
    [SerializeField] protected int thunder_amount = 5;
    [SerializeField] protected float thunder_attackRange = 10f;

    [Header("Magic Missile Attack")]
    [SerializeField] protected float missile_damage = 20f;
    [Tooltip("Determines time between missile shots")]
    [SerializeField] protected float missile_attackSpeed = 1f;
    [SerializeField] protected float missile_attackDelay = 10f;
    [SerializeField] protected float missile_attackRange = 12f;
    [SerializeField] protected int missile_amount = 6;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
