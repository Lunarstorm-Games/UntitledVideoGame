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
    [SaveField] bool hasBuilding;
    public void Interact(GameObject source)
    {
        BuildingUi.OpenWindow();
    }
    void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
       
        //BuildingUi = UIController.Instance.BuildingInterface;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void BuildStructure(BuildableStructure prefab)
    {
        if (CurrentBuilding == null)
        {
            EssenceBank.Instance?.SpendEssence(prefab.EssenceCost);
            GetComponent<MeshRenderer>().enabled = false;
            CurrentBuilding = Instantiate(prefab.gameObject,transform.position,transform.rotation,transform);
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
