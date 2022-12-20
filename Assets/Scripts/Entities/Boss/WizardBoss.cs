using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBoss : Entity
{
    [SerializeField] protected HealthBarUI healthBar;

    [Header("Passive stats")]
    [SerializeField] protected float speed = 4f;
    [SerializeField] protected float stoppingDistance = 7f;
    [SerializeField] protected float attackRange = 12f;
    [SerializeField] protected float attackDelay = 10f;

    [Header("Thunder Attack")]
    [SerializeField] protected float thunder_damage = 20f;
    [SerializeField] protected float thunder_speed = 7f;
    [Tooltip("The AOE from the thunder impact")]
    [SerializeField] protected float thunderAOEDamage = 1f;
    [Tooltip("How long the thunder AOE stays")]
    [SerializeField] protected float thunderAOETime = 4f;
    [Tooltip("Determines time between fireball shots")]
    [SerializeField] protected float thunder_attackSpeed = 1f;
    [SerializeField] protected Transform thunder_projectileSpawnPos;
    [SerializeField] protected Projectile thunder_projectile;
    [SerializeField] protected int thunder_amount = 5;

    [Header("Magic Missile Attack")]
    [SerializeField] protected float missile_damage = 20f;
    [SerializeField] protected float missile_speed = 7f;
    [Tooltip("Determines time between missile shots")]
    [SerializeField] protected float missile_attackSpeed = 1f;
    [SerializeField] protected int missile_amount = 6;
    [SerializeField] protected Transform missile_projectileSpawnPos;
    [SerializeField] protected Projectile missile_projectile;



    public Transform TargetSpot { get; set; }
    public Entity CurrentTarget { get; set; }
    public float PlayerRange { get { return Vector3.Distance(transform.position, Player.Instance.transform.position); } }
    public float Speed { get => speed; set => speed = value; }
    public float StoppingDistance { get => stoppingDistance; set => stoppingDistance = value; }
    public float AttackDelay { get => attackDelay; set => attackDelay = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public float Thunder_damage { get => thunder_damage; set => thunder_damage = value; }
    public float Thunder_speed { get => thunder_speed; set => thunder_speed = value; }
    public float ThunderAOEDamage { get => thunderAOEDamage; set => thunderAOEDamage = value; }
    public float ThunderAOETime { get => thunderAOETime; set => thunderAOETime = value; }
    public float Thunder_attackSpeed { get => thunder_attackSpeed; set => thunder_attackSpeed = value; }
    public Transform Thunder_projectileSpawnPos { get => thunder_projectileSpawnPos; set => thunder_projectileSpawnPos = value; }
    public Projectile Thunder_projectile { get => thunder_projectile; set => thunder_projectile = value; }
    public int Thunder_amount { get => thunder_amount; set => thunder_amount = value; }
    public float Missile_damage { get => missile_damage; set => missile_damage = value; }
    public float Missile_speed { get => missile_speed; set => missile_speed = value; }
    public float Missile_attackSpeed { get => missile_attackSpeed; set => missile_attackSpeed = value; }
    public int Missile_amount { get => missile_amount; set => missile_amount = value; }
    public Transform Missile_projectileSpawnPos { get => missile_projectileSpawnPos; set => missile_projectileSpawnPos = value; }
    public Projectile Missile_projectile { get => missile_projectile; set => missile_projectile = value; }
    

    public void Start()
    {
        CurrentTarget = Player.Instance;
        TargetSpot = Player.Instance.GetEntityTargetSpot();
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);

    }

    public override void TakeDamage(float damage, Entity origin)
    {

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0 && !Death)
        {
            Death = true;
        }
    }

    public void MissileProjectile()
    {
        Projectile projectileClone = Instantiate(missile_projectile, missile_projectileSpawnPos.position, Quaternion.identity);
        projectileClone.damage = missile_damage;
        projectileClone.speed = missile_speed;
        projectileClone.Initialize(this, Vector3.zero);
    }

    public void ThunderProjectile()
    {
        Projectile projectileClone = Instantiate(thunder_projectile, missile_projectileSpawnPos.position, Quaternion.identity);
        projectileClone.damage = thunder_damage;
        projectileClone.speed = thunder_speed;
        projectileClone.Initialize(this, Vector3.down);
    }
}
