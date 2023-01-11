using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class BufferedSpellProjectile : Projectile
    {
        private float seconds;
        public Action<BufferedSpellProjectile> OnCollision;
        public override void Initialize(Entity shooter, Vector3 direction)
        {
            transform.position = shooter.transform.position;
            DestroyProjectile(range);
            base.Initialize(shooter, direction);
        }

        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
        }

        public override void Start()
        {

        }

        public override void Update()
        {
            base.Update();
        }

        public override void DestroyProjectile(float delay = 0f)
        {
            StartCoroutine(DestroyDelay(delay));
               
            
        }
        IEnumerator DestroyDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
            OnCollision(this);
        }
        public override void ProjectileImpact()
        {
            base.ProjectileImpact();
            
            OnCollision(this);
        }

    }
}
