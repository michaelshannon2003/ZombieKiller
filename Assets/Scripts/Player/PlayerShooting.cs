﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Complete
{
    public class PlayerShooting : MonoBehaviour
    {
        #region EventPublisher
      //  public delegate void DisplayWeaponSwitch(string message);
      //  public static event DisplayWeaponSwitch OnWeaponEventMessage;
        #endregion EventPublisher


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

            EventManager.TriggerEvent("Message", "Switched to " + weaponlist[currentWeapon].name);
            
        }


        private void Fire()
        {

            //ScoreBoard.weapon = weaponlist[currentWeapon].name;
            // Set the fired flag so only Fire is only called once.
            var weapon = weaponlist[currentWeapon];
            
            
            // Create an instance of the shell and store a reference to it's rigidbody.
            Rigidbody shellInstance =
                Instantiate(weapon.GetComponent<Rigidbody>(), m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

            // Set the shell's velocity to the launch force in the fire position's forward direction.


            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = weapon.GetComponent<WeaponStats>().m_MinLaunchForce * m_FireTransform.forward;
        }
    }
}
