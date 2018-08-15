using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class Health : MonoBehaviour
    {

        #region EventPublisher
        public delegate void DisplayDeath(string message);
        public static event DisplayDeath OnHealthEventMessage;
        #endregion EventPublisher

        // public  Slider m_Slider;                             // The slider to represent how much health the tank currently has.
        private Image m_FillImage;                           // The image component of the slider.
        public Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
        public Color m_ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
      
        public float m_StartingHealth = 100f;                      // How much health the tank currently has.
        private float m_CurrentHealth;                      // How much health the tank currently has.
        [HideInInspector] public bool m_Dead;


        private void OnEnable()
        {
            // When the tank is enabled, reset the tank's health and whether or not it's dead.
            m_CurrentHealth = m_StartingHealth;
           
            // Update the health slider's value and color.
            SetHealthUI();
        }


        public void TakeDamage (float amount)
        {
            // Reduce current health by the amount of damage done.
            m_CurrentHealth -= amount;

            // Change the UI elements appropriately.
            SetHealthUI ();

            // If the current health is at or below zero and it has not yet been registered, call OnDeath.
            if (m_CurrentHealth <= 0f)
            {
                OnDeath ();
            }
        }


        private void SetHealthUI()
        {
            foreach (MeshRenderer render in gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                render.material.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
            }
        }


        // Used during the phases of the game where the player should be able to control their tank.
        public void OnDeath()
        {

            m_Dead = true;
            gameObject.SetActive(false);
            if (OnHealthEventMessage != null)
            {
                OnHealthEventMessage("A death occured !!!");
            }

        }
    }
}