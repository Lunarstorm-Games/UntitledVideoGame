using System;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts.Monobehaviour.Essence;
using CartoonFX;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TrapBuildingManager : MonoBehaviour
{
    public TrapModel[] traps;
    private GameObject pendingObject;
    private Vector3 pos;
    private RaycastHit hit;
    public bool canPlace;
    private EssenceBank essenceBank;
    [SerializeField] private float rotateAmount;
    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        essenceBank = EssenceBank.Instance;
    }

    void Update()
    {
        if (pendingObject != null)
        {
            pendingObject.transform.position = pos;

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceObject();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }

            UnselectTrap(pendingObject);
        }
    }
    
    private void PlaceObject()
    {
        int essenceCost = (int) traps[0].essenceCost;
            if (pendingObject.name == "Vortex(Clone)")
            {
                essenceCost = (int) traps[1].essenceCost;
            }
            if (essenceBank.SpendEssence(essenceCost))
            {
                pendingObject = null;
            }
    }s

    public void RotateObject()
    {
        pendingObject.transform.Rotate(Vector3.up, rotateAmount);
    }

    private void FixedUpdate()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }
    
    public void SelectObject(int index)
    {
        if (pendingObject != null)
        {
            Destroy(pendingObject);
        }
        pendingObject = Instantiate(traps[index].trapGameObject, pos, Quaternion.identity);
    }

    private void UnselectTrap(GameObject pendingObject)
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(pendingObject);
        }
    }
    
}