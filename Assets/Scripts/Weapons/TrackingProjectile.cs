using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class TrackingProjectile : BufferedSpellProjectile
    {
        private Enemy target;
        public override void Update()
        {
            if (target)
            {

            direction = (target.transform.position - transform.position).normalized;
           
            var step = speed * Time.deltaTime; // calculate distance to move
           
            transform.position = Vector3.MoveTowards(transform.position, target.GetEntityTargetSpot().position, step);
            }
            else { base.Update(); }

            
        }
        public void SetTarget(Enemy target)
        {
            this.target = target;
        }

    }
}
