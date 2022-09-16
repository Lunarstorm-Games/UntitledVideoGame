using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class BuildingRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask rayCollider;
    [SerializeField] private Transform ui;
    [SerializeField] private Transform playerOrigin;

    // UI ELEMENTS
    private GameObject etoBuiildPopUp;
    private bool isPromptOpen = false;

    void Start()
    {
        etoBuiildPopUp = ui.Find("EtoBuildPopUp").gameObject;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 200f, rayCollider))
        {
            if (hit.transform.CompareTag("BuildingSpot") && hit.distance <= 11f && !isPromptOpen)
            {
                ShowEtoBuildPopUp(true);
                etoBuiildPopUp.GetComponent<EtoBuild>().SetBuildSpot(hit.transform.parent);
                return;
            }
            ShowEtoBuildPopUp(false);
        }
        else
        {
            ShowEtoBuildPopUp(false);
        }
    }

    void ShowEtoBuildPopUp(bool status)
    {
        etoBuiildPopUp.SetActive(status);
    }

    public void SetIsPormptOpen(bool status)
    {
        isPromptOpen = status;
    }
}
