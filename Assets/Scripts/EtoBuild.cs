using System;
using System.Collections;
using System.Collections.Generic;
using FullscreenEditor;
using StarterAssets;
using UnityEngine;

public class EtoBuild : MonoBehaviour
{
    [SerializeField] private AreYouSurePrompt areYouSurePrompt;

    private GameObject buildSpot;

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            areYouSurePrompt.ShowPrompt();
        }
    }

    public GameObject GetBuildSpot()
    {
        return buildSpot;
    }

    public void SetGameSpot(GameObject go)
    {
        buildSpot = go;
    }
    
}
