using System;
using UnityEngine;

namespace Complete
{
    [Serializable]
    public class PlayerManager
    {
        // This class is to manage various settings on a tank.
        // It works with the GameManager class to control how the tanks behave
        // and whether or not players have control of their tank in the 
        // different phases of the game.

        public Color m_PlayerColor;                             // This is the color this tank will be tinted.
        public Transform m_SpawnPoint;                          // The position and direction the tank will have when it spawns.        
        [HideInInspector] public int m_PlayerNumber;            // This specifies which player this the manager for.
        [HideInInspector] public GameObject m_Instance;         // A reference to the instance of the tank when it is created.
        
        private PlayerMovement m_playerMovement;
        private PlayerShooting m_playerShooting;
        private GameObject m_CanvasGameObject;                  // Used to disable the world space UI during the Starting and Ending phases of each round.


        public void Setup ()
        {
          //  m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas> ().gameObject;
         
            m_playerMovement = m_Instance.GetComponent<PlayerMovement>();
            m_playerShooting = m_Instance.GetComponent<PlayerShooting>();

            m_playerMovement.m_PlayerNumber = m_PlayerNumber;        
            m_playerShooting.m_PlayerNumber = m_PlayerNumber;
            
            // Get all of the renderers of the tank.
            MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer> ();

            // Go through all the renderers...
            for (int i = 0; i < renderers.Length; i++)
            {

                // ... set their material color to the color specific to this tank.
                    renderers[i].material.color = m_PlayerColor;
            }

           
        }

      

        public void EnableControl(bool state)
        {
            m_playerMovement.enabled = state;
            m_playerShooting.enabled = state;           
        }
    }
}
