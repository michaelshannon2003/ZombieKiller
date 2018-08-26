using System;
using System.Linq;
using UnityEngine;

namespace Complete
{
    [Serializable]
    public class ZombieManager : LivingEntity
    {

        public ParticleSystem deathEffect;
        public ParticleSystem damageEffect;

        public void Start()
        {

            foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
            {

                renderer.material.color = Color.white;
            }
            base.Start();
        }



        // Used during the phases of the game where the player shouldn't be able to control their tank.      

        public override void TakeDamage(float damage)
        {


            if (damage >= health)
            {
                ShowDamageAnimation(deathEffect);
            }
            else {
                ShowDamageAnimation(damageEffect);
            }

            base.TakeDamage(damage);
        }



    }
}