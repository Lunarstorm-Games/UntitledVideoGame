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
    [SerializeField] private Material[] materials;
    [SerializeField] private float rotateAmount;
    [SerializeField] private LayerMask layerMask;
    public bool selected;

    private void Awake()
    {
        essenceBank = EssenceBank.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        if (pendingObject != null)
        {
            pendingObject.transform.position = pos;

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                Debug.Log("Object Placed");
                PlaceObject();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Destroy(pendingObject);
            }

            UpdateMaterials();
        }
    }
    
    void UpdateMaterials()
    {
        if (canPlace)
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[0];
        }

        if (!canPlace)
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[1];
        }
    }

    public void PlaceObject()
    {
        int essenceCost = (int) traps[0].essenceCost;
            if (pendingObject.name == "Vortex(Clone)")
            {
                essenceCost = (int) traps[1].essenceCost;
            }
            if (essenceBank.SpendEssence(essenceCost))
            {
                pendingObject.GetComponent<MeshRenderer>().material = materials[2];
                //pendingObject.transform.GetChild(0).gameObject.SetActive(false);
                pendingObject.name = pendingObject.name + " Placed";
                pendingObject = null;
            }
    }

    public void RotateObject()
    {
        pendingObject.transform.Rotate(Vector3.up, rotateAmount);
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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

    public void DestroyGameObject()
    {
        if (pendingObject == null)
        {
            Destroy(pendingObject);   
        }
    }
}