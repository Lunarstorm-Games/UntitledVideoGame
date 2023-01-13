using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WizardBoss : Entity
{
    [SerializeField] protected HealthBarUI healthBar;

    [Header("Passive stats")]
    [SerializeField] protected float speed = 4f;
    [SerializeField] protected float stoppingDistance = 7f;
    [SerializeField] protected float attackRange = 12f;

    [Header("Thunder Attack")]
    [SerializeField] protected float thunder_damage = 20f;
    [SerializeField] protected float thunder_speed = 7f;
    [Tooltip("The AOE from the thunder impact")]
    [SerializeField] protected float thunderAOEDamage = 1f;
    [Tooltip("How long the thunder AOE stays")]
    [SerializeField] protected float thunderAOETime = 4f;
    [SerializeField] protected float thunderAOETickRate = 2f;
    [SerializeField] protected float thunderAOERadius = 6f;
    [SerializeField] protected float thunder_attackSpeed = 1f;
    [SerializeField] protected WizardBossLightningProjectile thunder_projectile;
    [SerializeField] protected int thunder_amount = 5;
    [SerializeField] protected float thunder_attackDelay = 10f;

    [Header("Magic Missile Attack")]
    [SerializeField] protected float missile_damage = 20f;
    [SerializeField] protected float missile_speed = 7f;
    [Tooltip("Determines time between missile shots")]
    [SerializeField] protected float missile_attackSpeed = 1f;
    [SerializeField] protected int missile_amount = 6;
    [SerializeField] protected WizardBossMagicMissileProjectile missile_projectile;
    [SerializeField] protected float missile_attackDelay = 10f;



    public Transform TargetSpot { get; set; }
    public Entity CurrentTarget { get; set; }
    public float PlayerRange { get { return Vector3.Distance(transform.position, Player.Instance.transform.position); } }
    public float Speed { get => speed; set => speed = value; }
    public float StoppingDistance { get => stoppingDistance; set => stoppingDistance = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public float Thunder_AttackDelay { get => thunder_attackDelay; set => thunder_attackDelay = value; }
    public float Thunder_damage { get => thunder_damage; set => thunder_damage = value; }
    public float Thunder_speed { get => thunder_speed; set => thunder_speed = value; }
    public float ThunderAOEDamage { get => thunderAOEDamage; set => thunderAOEDamage = value; }
    public float ThunderAOETime { get => thunderAOETime; set => thunderAOETime = value; }
    public float Thunder_attackSpeed { get => thunder_attackSpeed; set => thunder_attackSpeed = value; }
    public WizardBossLightningProjectile Thunder_projectile { get => thunder_projectile; set => thunder_projectile = value; }
    public int Thunder_amount { get => thunder_amount; set => thunder_amount = value; }
    public float Missile_damage { get => missile_damage; set => missile_damage = value; }
    public float Missile_speed { get => missile_speed; set => missile_speed = value; }
    public float Missile_attackSpeed { get => missile_attackSpeed; set => missile_attackSpeed = value; }
    public int Missile_amount { get => missile_amount; set => missile_amount = value; }
    public float Missile_AttackDelay { get => missile_attackDelay; set => missile_attackDelay = value; }
    public WizardBossMagicMissileProjectile Missile_projectile { get => missile_projectile; set => missile_projectile = value; }
    public float CurrentAttackDelay { get; set; }

    public void Start()
    {
        CurrentTarget = Player.Instance;
        TargetSpot = Player.Instance.GetEntityTargetSpot();
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);

    }

    private void Update()
    {
        UpdateAttackDelay();
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

    public override void DeathAnimEvent()
    {
        base.DeathAnimEvent();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("EndingMenu", LoadSceneMode.Single);
    }

    public Projectile MissileProjectile(Vector3 spawnPos)
    {
        WizardBossMagicMissileProjectile projectileClone = Instantiate(missile_projectile, spawnPos, Quaternion.identity);
        projectileClone.SetTarget(CurrentTarget);
        projectileClone.damage = missile_damage;
        projectileClone.speed = missile_speed;
        projectileClone.Initialize(this, Vector3.zero);
        return projectileClone;
    }

    public Projectile ThunderProjectile(Vector3 spawnPos)
    {
        WizardBossLightningProjectile projectileClone = Instantiate(thunder_projectile, spawnPos, Quaternion.identity);
        projectileClone.damage = thunder_damage;
        projectileClone.speed = thunder_speed;
        projectileClone.AOE_Time = thunderAOETime;
        projectileClone.AOE_Damage = thunderAOEDamage;
        projectileClone.ThunderAOETickRate = thunderAOETickRate;
        projectileClone.ThunderAOERadius = thunderAOERadius;
        projectileClone.Initialize(this, Vector3.down);
        return projectileClone;
    }

    public void UpdateAttackDelay()
    {
        if (CurrentAttackDelay > 0)
        {
            CurrentAttackDelay -= Time.deltaTime;
        }
    }
}
