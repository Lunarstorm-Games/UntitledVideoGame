using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Defense
{
    public override void DeathAnimEvent()
    {
        gameObject.SetActive(false);
    }
}
