using UnityEngine;

namespace Complete
{
    public class ShellExplosion : MonoBehaviour
    {
        public float m_MaxLifeTime = 2f;                    // The time in seconds before the shell is removed.
        public float m_damagetaken = 50;

        private void Start ()
        {
            // If it isn't destroyed by then, destroy the shell after it's lifetime.
            Destroy (gameObject, m_MaxLifeTime);
        }


        private void OnTriggerEnter (Collider other)
        {
                // ... and find their rigidbody.
                if(other.gameObject.tag == "Zombies"){
                    other.GetComponentInParent<ZombieHealth>().TakeDamage(m_damagetaken);
                }
           
            Destroy(gameObject);
                
        }


    }
}