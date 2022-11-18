using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpotHighlight : MonoBehaviour, IInteractable
{
    public List<BuildableStructure> AllowedBuildings = new List<BuildableStructure>();
    private BuildingUIController BuildingUi;
    private GameObject CurrentBuilding = null;
    public void Interact(GameObject source)
    {
        BuildingUi.OpenWindow();
    }

    // Start is called before the first frame update
    void Start()
    {
        BuildingUi = UIController.Instance.BuildingInterface;
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void BuildStructure(BuildableStructure prefab)
    {
        if (CurrentBuilding == null)
        {
            GetComponent<MeshRenderer>().enabled = false;
            CurrentBuilding = Instantiate(prefab.gameObject,transform.position,transform.rotation, transform);
        }
    }
}
