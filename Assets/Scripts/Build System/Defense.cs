using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : Entity
{
    public override void TakeDamage(float damage, Entity origin)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !Death)
        {
            gameObject.SetActive(false);
        }
    }
}
