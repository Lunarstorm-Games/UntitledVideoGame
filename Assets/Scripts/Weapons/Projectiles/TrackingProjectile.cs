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
            
           
            var step = speed * Time.deltaTime; // calculate distance to move
           
            transform.position = Vector3.MoveTowards(transform.position, target.TargetSpot.position, step);
            
        }
        public void SetTarget(Enemy target)
        {
            this.target = target;
        }

    }
}
