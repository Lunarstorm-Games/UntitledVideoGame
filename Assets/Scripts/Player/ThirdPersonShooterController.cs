using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
    private SpellInventory spellInventory;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform spawnBulletPosition;
    private StarterAssetsInputs _starterAssetsInputs;
    private ThirdPersonController _thirdPersonController;
    private Animator _animator;
    private bool isPromptOpen = false;

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _animator = GetComponent<Animator>();

        spellInventory = GetComponent<SpellInventory>();
        projectilePrefab = spellInventory.spells[0];
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }
        
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
        
        if (_starterAssetsInputs.attack && !isPromptOpen)
        {
            _animator.SetLayerWeight(1,Mathf.Lerp(_animator.GetLayerWeight(1),1f,Time.deltaTime * 10f));
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Projectile projectile = GameObject.Instantiate<Projectile>(projectilePrefab, spawnBulletPosition.position, Quaternion.LookRotation(aimDir,Vector3.up));
            projectile.Initialize(this.GetComponent<Entity>(), aimDir);
            _starterAssetsInputs.attack = false;
        }
        else
        {
            _animator.SetLayerWeight(1,Mathf.Lerp(_animator.GetLayerWeight(1),0f,Time.deltaTime * 10f));
        }

        if (Input.inputString != "")
        {
            int number;
            bool isNumberKey = Int32.TryParse(Input.inputString, out number);
            if (isNumberKey)
            {
                if (number != 0 && spellInventory.spells[number - 1])
                        projectilePrefab = spellInventory.spells[number - 1];
                else if (spellInventory.spells[10])
                        projectilePrefab = spellInventory.spells[10];
            }
        }
    }
    
    public void SetIsPormptOpen(bool status)
    {
        isPromptOpen = status;
    }

    private void ChangeSpell()
    {

    }
}
