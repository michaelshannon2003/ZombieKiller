using System.Linq;
using UnityEngine;

namespace Complete
{
    public class ZombieMovement : MonoBehaviour
    {
        public float m_Speed = 1f;                 // How fast the tank moves forward and back.
        private Rigidbody m_Rigidbody;              // Reference used to move the tank.
       
        GameObject[] players;

        private void Awake ()
        {
            m_Rigidbody = GetComponent<Rigidbody> ();
            players = GameObject.FindGameObjectsWithTag("Player");
        }


        private void OnEnable ()
        {
            // When the tank is turned on, make sure it's not kinematic.
            m_Rigidbody.isKinematic = false;

            // Also reset the input values.
          
        }


        private void OnDisable ()
        {
            // When the tank is turned off, set it to kinematic so it stops moving.
            m_Rigidbody.isKinematic = true;

         
        }

    

        private void FixedUpdate()
        {
            
            Move ();
          
        }


        private void Move ()
        {
            GameObject closestPlayer = GetClosestPlayer();
             m_Rigidbody.transform.position = Vector3.MoveTowards(m_Rigidbody.transform.position, closestPlayer.transform.position, m_Speed * Time.deltaTime);
         
        }

        GameObject GetClosestPlayer()
        {
            return players.OrderBy(player => (player.transform.position - m_Rigidbody.transform.position).sqrMagnitude)
                           .FirstOrDefault();

        }



    }
}