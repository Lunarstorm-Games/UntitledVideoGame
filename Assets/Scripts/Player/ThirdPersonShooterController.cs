using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using TheraBytes.BetterUi;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform spawnBulletPosition;
    private float maxRayDistance = 999f;
    private StarterAssetsInputs _starterAssetsInputs;
    private ThirdPersonController _thirdPersonController;
    private Animator _animator;
    private bool isPromptOpen = false;
    private string currentSelectedSpell;
    private SpellInventory spellInventory;
    private SpellUI spellUI;
    private Vector3 mouseWorldPosition;
    //float lastShot;
    float nextFireTime = 0;

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _animator = GetComponent<Animator>();

        spellInventory = GetComponent<SpellInventory>();
        projectilePrefab = spellInventory.spells[0];

        spellUI = GameObject.Find("UI").GetComponentInChildren<SpellUI>();
    }

    private void Update()
    {
        mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, maxRayDistance, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }
        else
        {
            mouseWorldPosition = ray.origin + ray.direction * maxRayDistance;
        }

        // player actions
        Aim();
        ChangeSpell();
        CastSpell();
    }

    // cast the active spell
    void CastSpell()
    {
        //if (Time.time - lastShot < projectilePrefab.cooldown)
        //{
        //    return;
        //}
        if (Time.time > nextFireTime)
        {
            if (_starterAssetsInputs.attack && !isPromptOpen)
            {
                if (Player.Instance.currentMana >= projectilePrefab.mana)
                {
                    _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
                    Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                    Projectile projectile = GameObject.Instantiate<Projectile>(projectilePrefab, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                    projectile.Initialize(GetComponent<Entity>(), aimDir);
                    //lastShot= Time.time;
                    nextFireTime= Time.time + projectilePrefab.cooldown;
                    Player.Instance.UseMana(projectile.mana);
                    _starterAssetsInputs.attack = false;
                }
            }
            else
            {
                    _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            }
        }
    }

    // Changing the active spell with keys
    void ChangeSpell()
    {
        if (Input.inputString != "")
        {
            int number;
            bool isNumberKey = Int32.TryParse(Input.inputString, out number);
            if (isNumberKey)
            {
                if (number != 0 && spellInventory.spells[number - 1])
                {
                    projectilePrefab = spellInventory.spells[number - 1];
                    spellUI.SetActiveSpell(number);
                }
                else if (spellInventory.spells[10])
                    projectilePrefab = spellInventory.spells[10];
            }
        }
    }

    // Aiming wiht the player
    void Aim()
    {
        if (_starterAssetsInputs.aim && !isPromptOpen)
        {
            _virtualCamera.gameObject.SetActive(true);
            _thirdPersonController.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            _virtualCamera.gameObject.SetActive(false);
            _thirdPersonController.SetRotateOnMove(true);
        }
    }

    public void SetIsPormptOpen(bool status)
    {
        isPromptOpen = status;
    }
}
