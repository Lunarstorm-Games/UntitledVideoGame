using Assets.scripts.Monobehaviour.Essence;
using Assets.Scripts.Interfaces;
using Assets.Scripts.SaveSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpotHighlight : PersistableMonoBehaviour, IInteractable
{

    public List<BuildableStructure> AllowedBuildings = new List<BuildableStructure>();
    private BuildingUIController BuildingUi => UIController.Instance.BuildingInterface;
    private GameObject CurrentBuilding = null;
    [SaveField]public bool hasBuilding;
    public void Interact(GameObject source)
    {
        BuildingUi.OpenWindow();
    }

    internal void BuildStructure(BuildableStructure prefab)
    {
        if (CurrentBuilding == null)
        {
            EssenceBank.Instance?.SpendEssence(prefab.EssenceCost);
            GetComponent<MeshRenderer>().enabled = false;
            if (prefab.gameObject.name == "BuildableWall")
            {

            CurrentBuilding = Instantiate(prefab.gameObject,transform.position,transform.rotation,transform);
            }
            else
            {

                CurrentBuilding = Instantiate(prefab.gameObject, transform.position, transform.rotation);
            }
            CurrentBuilding.transform.parent = null;
            hasBuilding = true;
            gameObject.SetActive(false);

        }
    }

    public override void OnLoad()
    {
        gameObject.SetActive(!hasBuilding);
    }
}
