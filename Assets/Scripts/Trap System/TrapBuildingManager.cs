using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    private GameObject pendingObject;
    private Vector3 pos;
    private RaycastHit hit;
    [SerializeField]private float rotateAmount;

    [SerializeField]
    private LayerMask layerMask;
    
    // Update is called once per frame
    void Update()
    {
        if (pendingObject != null)
        {
            pendingObject.transform.position = pos;

            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }
        }
    }

    public void PlaceObject()
    {
        pendingObject = null;
    }

    public void RotateObject()
    {
        pendingObject.transform.Rotate(Vector3.up, rotateAmount);
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray,out hit,1000,layerMask))
        {
            pos = hit.point;

        }
    }

    public void SelectObject(int index)
    {
        pendingObject = Instantiate(objects[index], pos, Quaternion.identity);
    }
}
