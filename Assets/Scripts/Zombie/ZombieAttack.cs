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
            if (other.CompareTag("Player"))
            {
               other.GetComponentInParent<Health>().TakeDamage(m_damagetaken);
            }                
        }
    }
}