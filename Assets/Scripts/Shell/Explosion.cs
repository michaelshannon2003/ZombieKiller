using UnityEngine;

namespace Complete
{
    public class Explosion : MonoBehaviour
    {
        public float m_damagetaken = 100f;
        public float radius = 10f;
        public ParticleSystem explosionEffect;
        public LayerMask Layer;
        private float force = 1000f;
        private float upforce = 1.0f;

        private void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Bullet")
            {
                Instantiate(explosionEffect, transform.position, transform.rotation);
                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);


                foreach (Collider nearbyObject in colliders)
                {
                    Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(force, transform.position, radius, upforce);
                        OnHitObject(nearbyObject);
                    }

                }
            }
            Destroy(gameObject);
        }

        void OnHitObject(Collider c)
        {

            LivingEntity damageableObject = c.GetComponent<LivingEntity>();
            if (damageableObject != null)
            {
                damageableObject.TakeDamage(m_damagetaken);
            }          
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}