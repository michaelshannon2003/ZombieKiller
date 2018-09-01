﻿using UnityEngine;

namespace Complete
{
    public class LivingEntity : MonoBehaviour, IDamagable
    {
        public float startingHealth;
        protected float health;
        protected bool dead;

        public event System.Action OnDeath;

        public virtual void Start()
        {
            dead = false;
            health = startingHealth;
        }

        public virtual void TakeDamage(float damage)
        {

            health -= damage;
            if (health <= 0 && !dead)
            {

                Debug.Log(name + " is taking damage");
                Die();
            }
        }

        public virtual void ShowDamageAnimation(ParticleSystem particlesystem)
        {
            Destroy(Instantiate(particlesystem.gameObject, transform.position, transform.rotation) as GameObject, particlesystem.startLifetime);
        }

        [ContextMenu("Self Destruct")]
        public virtual void Die()
        {
            if (OnDeath != null)
            {
                OnDeath();

            }
        
            GameObject.Destroy(gameObject);

        }
    }
}