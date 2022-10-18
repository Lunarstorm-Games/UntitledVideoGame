using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    TrapBuildingManager _buildingManager;
    void Start()
    {
        _buildingManager = GameObject.Find("TrapBuildingManager").GetComponent<TrapBuildingManager>();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            _buildingManager.canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            _buildingManager.canPlace = true;
        }
    }
}
