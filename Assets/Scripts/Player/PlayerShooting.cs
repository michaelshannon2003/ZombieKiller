using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Complete
{
    public class PlayerShooting : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify the different players.

        public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
        public float m_MinLaunchForce = 30f;        // The force given to the shell if the fire button is not held.
        private string m_FireButton;                // The input axis that is used for launching shells.
        private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.        
        private List<GameObject> weaponlist;
        private int currentWeapon = 0;
        private Rigidbody m_Shell;                   // Prefab of the shell.

        private void OnEnable()
        {
            m_CurrentLaunchForce = m_MinLaunchForce;
         }

        private void Awake()
        {
            weaponlist = Resources.LoadAll<GameObject>("Prefabs/Weapons").ToList();
        }

        private void Start()
        {
           
            m_FireButton = "Fire" + m_PlayerNumber;
           
        }


        private void Update()
        {
           
            // The fire axis is based on the player number.
            Debug.Log("WeaponList " + weaponlist.Count());
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Switchweapons();
            }
            if (Input.GetButtonDown(m_FireButton))
            {
                Fire();
            }
        }



        private void Switchweapons()
        {
            if (currentWeapon == (weaponlist.Count - 1))
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }

        }


        private void Fire()
        {

            WeaponUsed.weapon = string.Format("Using {0}", weaponlist[currentWeapon].name);
            //ScoreBoard.weapon = weaponlist[currentWeapon].name;
            // Set the fired flag so only Fire is only called once.
            m_Shell = weaponlist[currentWeapon].GetComponent<Rigidbody>();

            // Create an instance of the shell and store a reference to it's rigidbody.
            Rigidbody shellInstance =
                Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

            // Set the shell's velocity to the launch force in the fire position's forward direction.


            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;
        }
    }
}
