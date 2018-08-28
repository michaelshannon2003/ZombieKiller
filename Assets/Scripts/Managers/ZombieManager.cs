using System;
using System.Linq;
using UnityEngine;

namespace Complete
{
    [Serializable]
    public class ZombieManager : LivingEntity
    {
        [Header ("Effects")]
        public ParticleSystem deathEffect;
        public ParticleSystem damageEffect;
        public AudioClip deathSound;
        public AudioClip damageSound;


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

            Debug.Log( "Zombie takes " + damage + " damage");
            if (damage >= health)
            {
                AudioManager.instance.PlaySound(deathSound);
                ShowDamageAnimation(deathEffect);
            }
            else
            {
                AudioManager.instance.PlaySound(damageSound);
                ShowDamageAnimation(damageEffect);
            }

            base.TakeDamage(damage);
        }

        public override void Die()
        {
          

           
            base.Die();


        }
    }
}