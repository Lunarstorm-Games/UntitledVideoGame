using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(ThirdPersonShooterController))]
public class Player : Entity, IDamageable
{
    [SerializeField] private HealthBarUI healthBar;

    [Header("Mana System")]
    [SerializeField] private ManaBar manaBar;
    [SerializeField] private float Mana;
    [SerializeField] private float manaRechargeRate = 3.5f;
    [SerializeField] public float currentMana;
    private PlayerInput playerInput;
    private ThirdPersonShooterController ThirdPersonShooterController;
    private StarterAssetsInputs starterAssetInputs;
    public static Player Instance { get; private set; }
    public override void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        ThirdPersonShooterController = GetComponent<ThirdPersonShooterController>();
        starterAssetInputs = GetComponent<StarterAssetsInputs>();
        base.Awake();

        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = Health;
        healthBar.SetMaxHealth(Health);
        currentMana = Mana;
        manaBar.SetMaxMana(Mana);
    }

    void Update()
    {
        RegenerateMana();
    }

    public override void TakeDamage(float damage, Entity origin)
    {
        if (!Killable)
            return;

        currentHealth -= damage;
        healthBar?.SetHealth(currentHealth);

        if (currentHealth <= 0 && !Death)
        {
            Death = true;
            OnDeath?.Invoke();
            Animator.SetTrigger("Death");
        }
    }

    public override void DeathAnimEvent()
    {
        //Revive System
    }

    public void UseMana(float mana)
    {
        currentMana -= mana;
        manaBar.SetMana(currentMana);
    }

    public void RegenerateMana()
    {
        if (currentMana < Mana)
        {
            currentMana += manaRechargeRate * Time.deltaTime * 10f;
            manaBar?.SetMana(currentMana);

        }
    }
    /// <summary>
    /// Toggles all input
    /// </summary>
    public void SetInputEnabled(bool value)
    {
        //playerInput.actions.Disable();
        if (value)
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerInput.ActivateInput();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            playerInput.DeactivateInput();
        }
        starterAssetInputs.cursorInputForLook = value;
        starterAssetInputs.cursorLocked = value;
      


    }
}
