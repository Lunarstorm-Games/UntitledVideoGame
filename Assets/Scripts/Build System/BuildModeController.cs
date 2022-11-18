using Assets.Scripts.Interfaces;
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
    private IInteractable interactable;
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
        
        if (Input.GetAxis("Interact") > 0)
        {
            interactable.Interact(gameObject);
        }
    }

    private void GetInteractable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, rayCollider))
        {
            if (!isPromptOpen)
            {
                var interactableObject = hit.transform.gameObject.GetComponents<MonoBehaviour>().OfType<IInteractable>().FirstOrDefault(); ;

                if (interactable != interactableObject) interactable = interactableObject;
                return;
            }
            interactable = null;
        }
       
    }
    public void BuildStructure(GameObject prefab)
    {
        var buildSpot = interactable as BuildingSpotHighlight;
        if (buildSpot != null)
        {
            buildSpot.BuildStructure(prefab);
        }
    }
}
