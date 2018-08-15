using System;
using System.Linq;
using UnityEngine;

namespace Complete
{
    [Serializable]
    public class ZombieManager
    {
     

        [HideInInspector] public int m_ZombieNumber;            // This specifies which player this the manager for.
        [HideInInspector] public GameObject m_Instance;         // A reference to the instance of the tank when it is created.
      
        
        //  private GameObject m_CanvasGameObject;                  // Used to disable the world space UI during the Starting and Ending phases of each round.


        public void Setup ()
        {

            // Go through all the renderers...
            foreach (MeshRenderer renderer in m_Instance.GetComponentsInChildren < MeshRenderer>())
            {
                
                renderer.material.color = Color.white;
            }
        }
       
    

        // Used during the phases of the game where the player shouldn't be able to control their tank.
        public void EnableControl (bool state)
        {

            m_Instance.SetActive (state);
        }



      
    }
}