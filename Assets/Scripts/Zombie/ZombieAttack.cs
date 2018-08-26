using UnityEngine;

namespace Complete
{
    public class ZombieAttack : MonoBehaviour
    {
       public int m_damagetaken = 100;


        private void Start ()
        {
          
        }


        private void OnTriggerEnter (Collider other)
        {
            if (!other.CompareTag(tag) && other.gameObject.layer != LayerMask.NameToLayer("Environment"))
            {
                OnHitObject(other);
            }
           

        }


        void OnHitObject(Collider c)
        {
            IDamagable damageableObject = c.GetComponent<IDamagable>();
            if (damageableObject != null)
            {

              
                damageableObject.TakeDamage(m_damagetaken);
            }           
        }

    }
}