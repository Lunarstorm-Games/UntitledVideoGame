using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildModeController : MonoBehaviour
{
    [SerializeField] private LayerMask rayCollider;
    public List<BuildableStructure> BuildableStructures;
    public float interactDistance = 20f;
    private GameObject etoBuildPopUp;
    private BuildingSpotHighlight interactable;
    private bool isPromptOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        etoBuildPopUp = UIController.Instance.transform.Find("EtoBuildPopUp").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetInteractable();

        if (Input.GetAxis("Interact") > 0 && interactable != null)
        {
            interactable.Interact(gameObject);
            UIController.Instance.BuildingInterface.BuildableStructures = interactable.AllowedBuildings.Select(x => x.gameObject).ToList() ;
        }
    }

    private void GetInteractable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, rayCollider))
        {

            var interactableObject = hit.transform.gameObject.GetComponents<BuildingSpotHighlight>().FirstOrDefault(); ;

            if (interactable != interactableObject) interactable = interactableObject;
            return;


        }
        interactable = null;

    }
    public void BuildStructure(BuildableStructure prefab)
    {
        var buildSpot = interactable as BuildingSpotHighlight;
        // the menu should be based on the buildable structures
        if (buildSpot != null && buildSpot.AllowedBuildings.Any(x=>x.gameObject.name ==prefab.name))
        {
            buildSpot.BuildStructure(prefab.GetComponent<BuildableStructure>());
        }
    }

    internal void Disable()
    {
        foreach(var buildingSpot in GameObject.FindGameObjectsWithTag("BuildingSpot")){
            buildingSpot.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
