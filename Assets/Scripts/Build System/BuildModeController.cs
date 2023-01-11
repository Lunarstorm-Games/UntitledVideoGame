using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.scripts.Monobehaviour.Essence;
using Assets.Scripts.Utility;
using UnityEngine;

public class BuildModeController : MonoBehaviour
{
    [SerializeField] private LayerMask rayCollider;
    public List<BuildableStructure> BuildableStructures;
    public float interactDistance = 20f;
    [SerializeField] private GameObject etoBuildPopUp;
    [ReadOnly] public BuildingSpotHighlight interactable;
    private bool isPromptOpen = false;


    // Update is called once per frame
    void Update()
    {
        if (!UIController.Instance?.BuildingInterface.gameObject.activeInHierarchy??false)
        {
            GetInteractable();
        }

        if (Input.GetAxis("Interact") > 0 && interactable != null)
        {
            interactable.Interact(gameObject);
            UIController.Instance.BuildingInterface.BuildableStructures = interactable.AllowedBuildings.Select(x => x.gameObject).ToList();
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
            etoBuildPopUp.SetActive(true);

            return;

        }
        interactable = null;
        etoBuildPopUp.SetActive(false);
    }

    void OnDrawGizmos()
    {
        if (interactable is not null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(interactable.transform.position, 1);
        }
    }
    public void BuildStructure(BuildableStructure prefab)
    {
        var buildSpot = interactable as BuildingSpotHighlight;
        // the menu should be based on the buildable structures
        if (buildSpot != null && buildSpot.AllowedBuildings.Any(x => x.gameObject.name == prefab.name))
        {
            if (EssenceBank.Instance?.EssenceAmount > prefab.EssenceCost)
            {

                EssenceBank.Instance?.SpendEssence(prefab.EssenceCost);
                buildSpot.BuildStructure(prefab.GetComponent<BuildableStructure>());
            }
        }
    }

    internal void Disable()
    {
        foreach (var buildingSpot in GameObject.FindGameObjectsWithTag("BuildingSpot"))
        {
            buildingSpot.SetActive(false);
        }
        etoBuildPopUp.SetActive(false);
        gameObject.SetActive(false);
    }
}
