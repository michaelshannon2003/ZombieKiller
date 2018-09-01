using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Complete
{
    [Serializable]
    public class ZombieManager : LivingEntity
    {
        [Header("Effects")]
        public ParticleSystem deathEffect;
        public ParticleSystem damageEffect;
        public AudioClip deathSound;
        public AudioClip damageSound;
        Animator animimator;
        bool is_not_dying;

        public void Start()
        {
           
            is_not_dying = false;
               animimator = GetComponent<Animator>();
            animimator.SetBool("IsDead", false);
            foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
            {

                renderer.material.color = Color.white;
            }
            base.Start();
        }



        // Used during the phases of the game where the player shouldn't be able to control their tank.      

        public override void TakeDamage(float damage)
        {

            if (damage >= health && !is_not_dying)
            {
                is_not_dying = true;
                StartCoroutine(DeathAnimations(damage));
            }
            else
            {
                AudioManager.instance.PlaySound(damageSound);
                ShowDamageAnimation(damageEffect);
                base.TakeDamage(damage);
            }

           
        }

        
        IEnumerator DeathAnimations(float damage)
        {
            animimator.SetBool("IsDead", true);
            animimator.SetTrigger("Dead");

            Debug.Log(animimator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            AudioManager.instance.PlaySound(deathSound);

            ShowDamageAnimation(deathEffect);
            StartCoroutine(WaitForDeathAnimation());
            yield return new WaitUntil(() => animimator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Dead"));

            Debug.Log(animimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
            yield return new WaitUntil(() => animimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f);


            Debug.Log(animimator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            base.TakeDamage(damage);
        }

        IEnumerator WaitForDeathAnimation()
        {
            Debug.Log(animimator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            yield return new WaitUntil(() => animimator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Dead"));

            Debug.Log(animimator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        }

    }
}