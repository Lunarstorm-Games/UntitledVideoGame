using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public override void Awake()
    {
        base.Awake();
        //add here extra states of behaviour
    }

    public override void Start()
    {
        base.Start();
        
    }

    public override void Update()
    {
        base.Update();
        Debug.Log(CurrentTarget.gameObject.transform.position);
        Debug.Log(FSM.CurrentState);
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
}
