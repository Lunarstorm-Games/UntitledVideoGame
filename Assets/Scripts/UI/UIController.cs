using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    private static UIController instance = null;
    public static UIController Instance => instance;
    public BuildingUIController BuildingInterface { get; private set; }
    public GameObject TimeLoopImage;
    private void Awake()
    {
        if (instance == null && !instance) instance = this;
        else if (instance != this)
        {
            Debug.LogWarning("Multiple instances of UIController present.");
            Destroy(this);
        }
    }
    void Start()
    {
      
        BuildingInterface = transform.Find("BuildingInterface").GetComponent<BuildingUIController>();
        //TimeLoopImage = transform.Find("TimeLoopPlayBackImage").gameObject;
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
