using System;
using System.Linq;
using UnityEngine;

namespace Complete
{
    [Serializable]
    public class ZombieManager
    {
        // This class is to manage various settings on a tank.
        // It works with the GameManager class to control how the tanks behave
        // and whether or not players have control of their tank in the 
        // different phases of the game.

        //    public Color m_PlayerColor;                             // This is the color this tank will be tinted.
        //    public Transform m_SpawnPoint;    
        // The position and direction the tank will have when it spawns.      
        [HideInInspector] public int m_ZombieNumber;            // This specifies which player this the manager for.
        [HideInInspector] public GameObject m_Instance;         // A reference to the instance of the tank when it is created.
      
        
        //  private GameObject m_CanvasGameObject;                  // Used to disable the world space UI during the Starting and Ending phases of each round.


        public void Setup ()
        {

            //  _CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject.Se;
        
              // Get all of the renderers of the tank.
              MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer> ();

            // Go through all the renderers...
            for (int i = 0; i < renderers.Length; i++)
            {
                // ... set their material color to the color specific to this tank.
                renderers[i].material.color = Color.white;
            }

         
        }
       
    

        // Used during the phases of the game where the player shouldn't be able to control their tank.
        public void DisableControl ()
        {

            m_Instance.SetActive (false);
        }


        // Used during the phases of the game where the player should be able to control their tank.
        public void EnableControl ()
        {

            m_Instance.SetActive (true);
        }

     

       


    }
}