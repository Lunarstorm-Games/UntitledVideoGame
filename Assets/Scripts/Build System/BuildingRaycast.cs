using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class BuildingRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask rayCollider;
    [SerializeField] private Transform ui;

    // UI ELEMENTS
    private GameObject etoBuildPopUp;
    private GameObject etoUpgradePopUp;
    private GameObject etoUpgradeTrapPopUp;
    private bool isPromptOpen = false;

    void Awake()
    {
        etoBuildPopUp = ui.Find("EtoBuildPopUp").gameObject;
        etoUpgradePopUp = ui.Find("EtoUpgradePopUp").gameObject;
        etoUpgradeTrapPopUp = ui.Find("EtoUpgradeTrapPopUp").gameObject;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 200f, rayCollider))
        {
            if (hit.distance <= 11f && !isPromptOpen)
            {
                if (hit.transform.CompareTag("BuildingSpot"))
                {
                    ShowEtoBuildPopUp(true);
                    etoBuildPopUp.GetComponent<EtoAction>().SetBuildSpot(hit.transform.parent);
                    return;
                }
                if (hit.transform.CompareTag("MagicWorkshop"))
                {
                    ShowEtoUpgradePopUp(true);
                    etoUpgradePopUp.GetComponent<EtoAction>().SetBuilding(hit.transform.parent);
                    return;
                }
                if (hit.transform.CompareTag("TrapWorkshop"))
                {
                    ShowEtoUpgradeTrapPopUp(true);
                    etoUpgradeTrapPopUp.GetComponent<EtoAction>().SetBuilding(hit.transform.parent);
                    return;
                }
            }
            ShowEtoBuildPopUp(false);
            ShowEtoUpgradePopUp(false);
            ShowEtoUpgradeTrapPopUp(false);
        }
        else
        {
            ShowEtoBuildPopUp(false);
            ShowEtoUpgradePopUp(false);
            ShowEtoUpgradeTrapPopUp(false);
        }
    }

    void ShowEtoBuildPopUp(bool status)
    {
        etoBuildPopUp.SetActive(status);
    }

    void ShowEtoUpgradePopUp(bool status)
    {
        etoUpgradePopUp.SetActive(status);
    }
    
    void ShowEtoUpgradeTrapPopUp(bool status)
    {
        etoUpgradeTrapPopUp.SetActive(status);
    }

    public void SetIsPormptOpen(bool status)
    {
        isPromptOpen = status;
    }
}