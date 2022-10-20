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
    public GameObject[] objects;
    public int[] essenceCost;
    private GameObject pendingObject;
    private Vector3 pos;
    private RaycastHit hit;
    public bool canPlace;
    private EssenceBank essenceBank;
    [SerializeField] private Transform warning;
    [SerializeField] private Material[] materials;
    [SerializeField] private float rotateAmount;
    [SerializeField] private LayerMask layerMask;
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
        if (essenceBank.SpendEssence(essenceCost[0]))
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[2];
            //pendingObject.transform.GetChild(0).gameObject.SetActive(false);
            pendingObject.name = pendingObject.name + " Placed";
            pendingObject = null;
        }
        else
        {
            
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
        pendingObject = Instantiate(objects[index], pos, Quaternion.identity);
    }
}