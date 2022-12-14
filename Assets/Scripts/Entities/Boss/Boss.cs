
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Entity
{
    [Header("Flying State")]
    [SerializeField] protected float fly_speed = 4f;
    [SerializeField] protected float fly_stoppingDistance = 7f;

    [Header("Ground State")]
    [SerializeField] protected float ground_speed = 4f;
    [SerializeField] protected float ground_stoppingDistance = 7f;

    [Header("Fireball Attack")]
    [SerializeField] protected float fireball_damage = 20f;
    [SerializeField] protected float fireball_speed = 7f;
    [Tooltip("The fire from the firebal impact")]
    [SerializeField] protected float fireTickDamage = 1f;
    [Tooltip("How long the fire stays")]
    [SerializeField] protected float fireTime = 4f;
    [Tooltip("Determines time between fireball shots")]
    [SerializeField] protected float fireball_attackSpeed = 1f;
    [SerializeField] protected float fireball_attackDelay = 10f;
    [SerializeField] protected Transform projectileSpawnPos;
    [SerializeField] protected Projectile projectile;
    [SerializeField] protected int fireball_amount = 3;
    [SerializeField] protected float fireball_attackRange = 10f;

    [Header("Lunge Attack")]
    [SerializeField] protected float lunge_damage = 20f;
    [SerializeField] protected float lunge_attackSpeed = 1f;
    [SerializeField] protected float lunge_attackDelay = 10f;
    [SerializeField] protected float lunge_attackRange = 4f;


    public Transform TargetSpot { get; set; }
    public Entity CurrentTarget { get; set; }
    public float Fly_speed { get => fly_speed; set => fly_speed = value; }
    public float Fly_stoppingDistance { get => fly_stoppingDistance; set => fly_stoppingDistance = value; }
    public float Ground_speed { get => ground_speed; set => ground_speed = value; }
    public float Ground_stoppingDistance { get => ground_stoppingDistance; set => ground_stoppingDistance = value; }
    public float Fireball_damage { get => fireball_damage; set => fireball_damage = value; }
    public float Fireball_speed { get => fireball_speed; set => fireball_speed = value; }
    public float FireTickDamage { get => fireTickDamage; set => fireTickDamage = value; }
    public float FireTime { get => fireTime; set => fireTime = value; }
    public float Fireball_attackSpeed { get => fireball_attackSpeed; set => fireball_attackSpeed = value; }
    public float Fireball_attackDelay { get => fireball_attackDelay; set => fireball_attackDelay = value; }
    public float Lunge_damage { get => lunge_damage; set => lunge_damage = value; }
    public float Lunge_attackSpeed { get => lunge_attackSpeed; set => lunge_attackSpeed = value; }
    public float Lunge_attackDelay { get => lunge_attackDelay; set => lunge_attackDelay = value; }
    public Transform ProjectileSpawnPos { get => projectileSpawnPos; set => projectileSpawnPos = value; }
    public Projectile Projectile { get => projectile; set => projectile = value; }
    public int Fireball_amount { get => fireball_amount; set => fireball_amount = value; }
    public float Lunge_attackRange { get => lunge_attackRange; set => lunge_attackRange = value; }
    public float Fireball_attackRange { get => fireball_attackRange; set => fireball_attackRange = value; }
    public float PlayerRange { get { return Vector3.Distance(transform.position, Player.Instance.transform.position); } }

    public void Start()
    {
        CurrentTarget = Player.Instance;
        TargetSpot = Player.Instance.GetEntityTargetSpot();
    }

    public void FireballProjectile()
    {
        Projectile projectileClone = Instantiate(projectile, projectileSpawnPos.position, Quaternion.identity);
        Vector3 TargetPos = TargetSpot.position;
        Vector3 shootDir = (TargetPos - projectileSpawnPos.position).normalized;
        projectileClone.transform.LookAt(TargetPos);
        projectileClone.damage = fireball_damage;
        projectileClone.speed = fireball_speed;
        projectileClone.Initialize(this, shootDir);
    }
}
