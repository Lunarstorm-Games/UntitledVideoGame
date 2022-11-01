using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Conversation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = this.transform.position - Camera.main.transform.position;
        dir.y = 0f;
        this.transform.rotation = Quaternion.LookRotation(dir);
    }
}
