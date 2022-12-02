using Assets.Scripts.Projectiles;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : Entity
{
    public GameObject projectilePrefab;
    private List<BufferedSpellProjectile> ProjectileBuffer = new List<BufferedSpellProjectile>();
    private List<BufferedSpellProjectile> ActiveProjectiles = new List<BufferedSpellProjectile>();
    public Transform ShootingOrigin;
    public int initialProjectileInstances;
    public int range = 20;
    public float secondsBetweenShots = 1f;
    // Start is called before the first frame update
  protected  void Start()
    {
        for (int i = 0; i < initialProjectileInstances; i++)
        {
            var newProjectile = Instantiate(projectilePrefab);
            ProjectileBuffer.Add(newProjectile.GetComponent<BufferedSpellProjectile>());
            newProjectile.gameObject.SetActive(false);
            newProjectile.GetComponent<BufferedSpellProjectile>().OnCollision += OnProjectileDestroy;

        }
        StartCoroutine(TargetingCoroutine());
    }

    // Update is called once per frame

  protected  void Update()
    {

    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(255, 252, 0, 0.12f);

        Gizmos.DrawSphere(transform.position, range);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(ShootingOrigin.position, 0.1f);
    }
    IEnumerator TargetingCoroutine()
    {
        while (gameObject.activeInHierarchy)
        {

            ShootProjectile();
            yield return new WaitForSeconds(secondsBetweenShots);
        }
    }
   protected virtual void ShootProjectile()
    {
        var target = GetClosestEnemy()?.GetComponent<Enemy>();

        if (target is null) return;

        var direction = (target.transform.position - transform.position).normalized;

        BufferedSpellProjectile projectile = GetProjectileFromBuffer();
        (projectile as TrackingProjectile)?.SetTarget(target);
        projectile.gameObject.SetActive(true);


        projectile.Initialize(this, direction);
        projectile.transform.position = ShootingOrigin.position;

    }

    protected BufferedSpellProjectile GetProjectileFromBuffer()
    {
        var projectile = ProjectileBuffer.FirstOrDefault();
        if (projectile == null)
        {
            projectile = Instantiate(projectilePrefab).GetComponent<BufferedSpellProjectile>();
        }
        else
        {
            ProjectileBuffer.Remove(projectile);
        }
        ActiveProjectiles.Add(projectile);
        return projectile;
    }

    protected GameObject GetClosestEnemy()
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
            if (curDistance < distance)
            {
                if (Physics.Raycast(position, diff, out var hit, range))
                {
                    if (hit.collider.gameObject == go)
                    {
                        closest = go;
                        distance = curDistance;
                    }
                }

            }
        }
        return closest;
    }
    public  virtual void OnProjectileDestroy(BufferedSpellProjectile source)
    {
        ActiveProjectiles.Remove(source);
        ProjectileBuffer.Add(source);

    }
    public override void DeathAnimEvent()
    {
        gameObject.SetActive(false);
    }
}
