using UnityEngine;

namespace Complete
{
    public class WeaponStats : MonoBehaviour
    {
        public float m_MinLaunchForce = 30f;        // The force given to the shell if the fire button is not held.
        public float fireRate;
        public int ClipCapacity = 1;
        public float reloadSpeed = 1;
    }
}