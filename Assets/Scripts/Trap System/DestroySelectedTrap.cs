using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Assets.scripts.Monobehaviour.Essence;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroySelectedTrap : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    // public GameObject vortex;
    // public GameObject light;
    // private EssenceBank essenceBank;
    // [SerializeField] private TrapModel vortexModel;
    // [SerializeField] private TrapModel lightModel;
    // private void Awake()
    // {
    //     essenceBank = EssenceBank.Instance;
    // }
    //
    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     vortex = GameObject.Find("Vortex(Clone)");
    //     light = GameObject.Find("GroundMagic(Clone)");
    //
    // }
    //
    // public void OnPointerClick(PointerEventData eventData)
    // {
    //     if (vortex)
    //     {
    //         Destroy(vortex);
    //         essenceBank.AddEssence((int) vortexModel.essenceCost);
    //     }
    //     else if(light)
    //     {
    //         Destroy(light);
    //         essenceBank.AddEssence((int) lightModel.essenceCost);
    //     }
    // }
    
    TrapBuildingManager _buildingManager;
    void Start()
    {
        _buildingManager = GameObject.Find("TrapBuildingManager").GetComponent<TrapBuildingManager>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _buildingManager.selected = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _buildingManager.selected = false;
    }
}
