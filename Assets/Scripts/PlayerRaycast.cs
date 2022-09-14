using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask rayCollider;
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform ui;

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
        
        if (Physics.Raycast(ray, out hit, 20f, rayCollider))
        {
            debugTransform.gameObject.SetActive(true);
            debugTransform.position = hit.point;
            if (hit.transform.CompareTag("BuildingSpot") && hit.distance <= 7f && !isPromptOpen)
            {
                ShowEtoBuildPopUp(true);
                etoBuiildPopUp.GetComponent<EtoBuild>().SetGameSpot(hit.transform.gameObject);
                return;
            }
            ShowEtoBuildPopUp(false);
        }
        else
        {
            debugTransform.gameObject.SetActive(false);
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
