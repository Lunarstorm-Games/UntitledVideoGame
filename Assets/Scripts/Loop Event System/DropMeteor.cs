using System;
using UnityEngine;
using Object = System.Object;

public class DropMeteor : MonoBehaviour
{
    public GameObject Item;
    public int Speed;
    public Vector3 TargetPos;

    private void Update()
    {
        var step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, step);
        
        if (Vector3.Distance(transform.position, TargetPos) < 0.001f)
        {
            Instantiate(Item, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
