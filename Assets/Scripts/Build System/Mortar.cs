using Assets.Scripts.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : Turret
{
    /// <summary>
    /// minimum range of mortar
    /// </summary>
    float minRange = 20;
    float HeightAboveEnemy = 15;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void ShootProjectile()
    {
        var target = GetClosestEnemy()?.GetComponent<Enemy>();

        if (target is null) return;
        var shootingOriginPosition = target.transform.position + new Vector3(0, HeightAboveEnemy, 0);
        ShootingOrigin.position = shootingOriginPosition;
        var direction = (target.transform.position - ShootingOrigin.position).normalized;

        BufferedSpellProjectile projectile = GetProjectileFromBuffer();
        var mortarprojectile = projectile as MortarProjectile;
        projectile.gameObject.SetActive(true);


        mortarprojectile.Initialize(this, direction);
        projectile.transform.position = ShootingOrigin.position;
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(255, 252, 0, 0.12f);

        Gizmos.DrawSphere(transform.position, range);

       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(ShootingOrigin.position, 1f);
    }
    protected new GameObject GetClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = range;
        Vector3 position = ShootingOrigin.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.magnitude;
            if (curDistance < distance && curDistance> minRange)
            {


                closest = go;
                distance = curDistance;



            }
        }
        return closest;
    }
}
