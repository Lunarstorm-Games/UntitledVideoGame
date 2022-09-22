using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    void Initialize(Entity origin, float speed, float damage, Vector3 moveDir, float lifeTime);

    void Update();

    void OnTriggerEnter(Collider collider);
}
