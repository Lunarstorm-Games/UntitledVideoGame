using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    private static UIController instance = null;
    public static UIController Instance => instance;
    public BuildingUIController BuildingInterface { get; private set; }
    void Start()
    {
        if (instance == null &&!instance) instance = this;
        else if (instance != this)
        {
            Debug.LogWarning("Multiple instances of UIController present.");
            Destroy(this);
        }
        BuildingInterface = transform.Find("BuildingInterface").GetComponent<BuildingUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        instance = null;
    }
}
