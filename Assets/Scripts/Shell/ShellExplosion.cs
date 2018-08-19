using UnityEngine;

namespace Complete
{
    public class ShellExplosion : MonoBehaviour
    {
        public float m_MaxLifeTime = 2f;                    // The time in seconds before the shell is removed.
        public float m_damagetaken = 50;

        private void Start()
        {
            Destroy(gameObject, m_MaxLifeTime);
        }


        private void OnTriggerEnter(Collider other)
        {

            if (!other.CompareTag(tag) && other.gameObject.layer != LayerMask.NameToLayer("Environment"))
            {

                Health opponentheath = other.GetComponentInParent<Health>();

                if (opponentheath == null) { Debug.Log(other.name); }
                else { opponentheath.GetComponentInParent<Health>().TakeDamage(m_damagetaken); }
                
                
            }

                Destroy(gameObject);
         
        }


    }
}