using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralObjective : Entity, IDamageable
{
    public float health = 1000;
    public void TakeDamage(float damage, Entity origin)
    {
        if (health > 0) health -= damage;
        if(health < 0) health = 0;
        if(health <= 0) ObjectiveDestroyed();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ObjectiveDestroyed()
    {
            GameController.instance.StartTimeLoop();
    }
}
