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

            if (!other.CompareTag(tag))
            {
                other.GetComponentInParent<Health>().TakeDamage(m_damagetaken);
            }

            Destroy(gameObject);

        }


    }
}