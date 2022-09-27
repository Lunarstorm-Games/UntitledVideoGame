using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratingProjectile : Projectile
{
    [SerializeField] private Vector3 velocity;

    public override void Initialize(Entity origin, Vector3 direction)
    {
        base.Initialize(origin, direction);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        velocity += direction * speed * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }

    protected override void DestroyProjectile(float delay = 0f)
    {
        base.DestroyProjectile(delay);
    }
}
