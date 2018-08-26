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
                    OnHitObject(other);               
            }
                Destroy(gameObject);
         
        }


        void OnHitObject(Collider c)
        {

            Debug.Log("Hitting " + c.name);
            IDamagable damageableObject = c.GetComponent<IDamagable>();
            if (damageableObject != null)
            {

                Debug.Log("Taking damage " + c.name);
                damageableObject.TakeDamage(m_damagetaken);
            }
            else {
                Debug.Log("NO Idaamagel");
            }
        }


    }
}