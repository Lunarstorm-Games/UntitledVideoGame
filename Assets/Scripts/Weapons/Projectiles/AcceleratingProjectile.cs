using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratingProjectile : Projectile
{
    [SerializeField] private Vector3 velocity;

    public override void Update()
    {
        velocity += direction * speed * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }
}
