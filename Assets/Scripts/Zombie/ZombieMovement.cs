using System.Linq;
using UnityEngine;

namespace Complete
{
    public class ZombieMovement : MonoBehaviour
    {
        public float m_Speed = 1f;                 // How fast the tank moves forward and back.
        private Rigidbody m_Rigidbody;              // Reference used to move the tank.
        private bool hasTarget;
        PlayerManager player;

        private void Awake ()
        {
            m_Rigidbody = GetComponent<Rigidbody> ();
            // player = GameObject.FindGameObjectWithTag("Player");
            player = FindObjectOfType<PlayerManager>() ;
            if (player != null)
            {
                hasTarget = true;
                player.OnDeath += OnTargetDeath;
            }
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

        void OnTargetDeath()
        {
            hasTarget = false;           
        }


        private void FixedUpdate()
        {
            if (hasTarget) { Move(); }
            
          
        }


        private void Move ()
        {
             m_Rigidbody.transform.position = Vector3.MoveTowards(m_Rigidbody.transform.position, player.transform.position, m_Speed * Time.deltaTime);
            transform.LookAt(player.transform);

        }




    }
}