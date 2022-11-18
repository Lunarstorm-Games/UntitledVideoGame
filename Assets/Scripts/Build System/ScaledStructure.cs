using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This object scales to the size of its parent
/// </summary>
public class ScaledStructure : BuildableStructure
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.parent
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
