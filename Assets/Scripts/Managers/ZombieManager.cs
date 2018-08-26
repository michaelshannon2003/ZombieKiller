using System;
using System.Linq;
using UnityEngine;

namespace Complete
{
    [Serializable]
    public class ZombieManager :  LivingEntity
    {
     

        [HideInInspector] public int m_ZombieNumber;            // This specifies which player this the manager for.
        [HideInInspector] public GameObject m_Instance;         // A reference to the instance of the tank when it is created.

        public void OnStart ()
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

        public override void TakeDamage(float damage)
        {

            Debug.Log("Zombie takes damage " + damage);
            if (damage >= health)
            {
            //    Destroy(Instantiate(deathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, deathEffect.startLifetime);
            }
            base.TakeDamage(damage);
        }



    }
}