using UnityEngine;

namespace Complete
{
    public class ZombieAttack : MonoBehaviour
    {
       


        private void Start ()
        {
          
        }


        private void OnTriggerEnter (Collider other)
        {

                // ... and find their rigidbody.
                if(other.gameObject.tag == "Player"){   
                   PlayerHealth targetHealth = other.gameObject.GetComponentInParent<Rigidbody>().GetComponent<PlayerHealth> ();
                   targetHealth.OnDeath();
                }
                       
                
        }


    }
}