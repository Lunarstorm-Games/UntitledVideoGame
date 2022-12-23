
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonBoss : Entity
{
    [SerializeField] protected HealthBarUI healthBar;

    [Header("States")]
    [Tooltip("Delay between switching from flying to ground state.")]
    [SerializeField] protected float switchCooldown;
    [SerializeField] protected bool canSwitch;

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
    [SerializeField] protected float fireAOETickRate = 2f;
    [SerializeField] protected float fireAOERadius = 6f;
    [Tooltip("Determines time between fireball shots")]
    [SerializeField] protected float fireball_attackSpeed = 1f;
    [SerializeField] protected float fireball_attackDelay = 10f;
    [SerializeField] protected Transform projectileSpawnPos;
    [SerializeField] protected DragonFireballProjectile projectile;
    [SerializeField] protected int fireball_amount = 3;
    [SerializeField] protected float fireball_attackRange = 10f;

    [Header("Lunge Attack")]
    [SerializeField] protected float lunge_damage = 20f;
    [SerializeField] protected float lunge_attackSpeed = 1f;
    [SerializeField] protected float lunge_attackDelay = 10f;
    [SerializeField] protected float lunge_attackRange = 4f;

    [Header("Bite Attack")]
    [SerializeField] protected float bite_damage = 20f;
    [SerializeField] protected float bite_attackSpeed = 1f;
    [SerializeField] protected float bite_attackDelay = 10f;
    [SerializeField] protected float bite_attackRange = 4f;

    [Header("Second Phase")]
    [SerializeField] protected GameObject evilWizard_Prefab;
    [SerializeField] protected Transform spawnPos;


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
    public float FireAOETickRate { get => fireAOETickRate; set => fireAOETickRate = value; }
    public float FireAOERadius { get => fireAOERadius; set => fireAOERadius = value; }
    public float Fireball_attackSpeed { get => fireball_attackSpeed; set => fireball_attackSpeed = value; }
    public float Fireball_attackDelay { get => fireball_attackDelay; set => fireball_attackDelay = value; }
    public float Lunge_damage { get => lunge_damage; set => lunge_damage = value; }
    public float Lunge_attackSpeed { get => lunge_attackSpeed; set => lunge_attackSpeed = value; }
    public float Lunge_attackDelay { get => lunge_attackDelay; set => lunge_attackDelay = value; }
    public Transform ProjectileSpawnPos { get => projectileSpawnPos; set => projectileSpawnPos = value; }
    public DragonFireballProjectile Projectile { get => projectile; set => projectile = value; }
    public int Fireball_amount { get => fireball_amount; set => fireball_amount = value; }
    public float Lunge_attackRange { get => lunge_attackRange; set => lunge_attackRange = value; }
    public float Fireball_attackRange { get => fireball_attackRange; set => fireball_attackRange = value; }
    public float PlayerRange { get { return Vector3.Distance(transform.position, Player.Instance.transform.position); } }
    public float Bite_damage { get => bite_damage; set => bite_damage = value; }
    public float Bite_attackSpeed { get => bite_attackSpeed; set => bite_attackSpeed = value; }
    public float Bite_attackDelay { get => bite_attackDelay; set => bite_attackDelay = value; }
    public float Bite_attackRange { get => bite_attackRange; set => bite_attackRange = value; }
    public bool CanSwitch { get => canSwitch; set => canSwitch = value; }
    public float CurrentAttackDelay { get; set; }
    public float CurrentBiteAttackDelay { get; set; }

    private float switchCooldownTime;

    public void Start()
    {
        CurrentTarget = Player.Instance;
        TargetSpot = Player.Instance.GetEntityTargetSpot();
        switchCooldownTime = switchCooldown;
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);

    }

    public void Update()
    {
        SwitchStateCooldown();
        UpdateAttackDelay();
        UpdateBiteAttackDelay();
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

    public void FireballProjectile()
    {
        DragonFireballProjectile projectileClone = Instantiate(projectile, projectileSpawnPos.position, Quaternion.identity);
        Vector3 TargetPos = TargetSpot.position;
        Vector3 shootDir = (TargetPos - projectileSpawnPos.position).normalized;
        projectileClone.transform.LookAt(TargetPos);
        projectileClone.damage = fireball_damage;
        projectileClone.speed = fireball_speed;
        projectileClone.AOE_Time = fireTime;
        projectileClone.AOE_Damage = fireTickDamage;
        projectileClone.FireAOETickRate = fireAOETickRate;
        projectileClone.FireAOERadius = fireAOERadius;
        projectileClone.Initialize(this, shootDir);
    }

    public override void DeathAnimEvent()
    {
        Debug.LogError("Event");
        Instantiate(evilWizard_Prefab, spawnPos.position, Quaternion.identity);
        base.DeathAnimEvent();
    }

    private void SwitchStateCooldown()
    {
        if (switchCooldownTime < 0f && !canSwitch)
        {
            switchCooldownTime = switchCooldown;
            canSwitch = true;
        }
        else
        {
            switchCooldownTime -= Time.deltaTime;
        }
    }

    public void UpdateAttackDelay()
    {
        if (CurrentAttackDelay > 0)
        {
            Debug.Log(CurrentAttackDelay);
            CurrentAttackDelay -= Time.deltaTime;
        }
    }

    public void UpdateBiteAttackDelay()
    {
        if (CurrentBiteAttackDelay > 0)
        {
            CurrentBiteAttackDelay -= Time.deltaTime;
        }
    }
}
