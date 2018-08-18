using System.Collections;
using System.Collections.Generic;
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
        private string m_FireButton;                // The input axis that is used for launching shells.

       private List<GameObject> weaponlist;
        private int currentWeapon = 0;        

        private bool able_to_fire = true;
        private int remainingbullets;
        private WeaponStats selectedWeaponStats;
        private GameObject selectedWeapon;

        private void OnEnable()
        {          
        }

        private void Awake()
        {
            weaponlist = Resources.LoadAll<GameObject>("Prefabs/Weapons").ToList();            
        }

        private void Start()
        {
            m_FireButton = "Fire" + m_PlayerNumber;
            SelectedWeaponStats();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Switchweapons();
            }
            if (Input.GetButtonDown(m_FireButton) && able_to_fire){
                StartCoroutine(FiringLoop());
            }
        }

        private IEnumerator FiringLoop()
        {

            if (remainingbullets == 0)
            {                
                yield return StartCoroutine(Reload());                
            }

            if (able_to_fire)
            {                
                yield return StartCoroutine(ShootBullet());               
            }           
        }

        private IEnumerator Reload()
        {
            able_to_fire = false;
            EventManager.TriggerEvent("Message", "Reloading weapon");          
            yield return new WaitForSeconds(selectedWeaponStats.reloadSpeed);
            remainingbullets = selectedWeaponStats.ClipCapacity;
            able_to_fire = true;
        }

        private void Switchweapons()
        {

            if (currentWeapon == (weaponlist.Count - 1))
            {
                currentWeapon =  0;
                
            }
            else
            {
                currentWeapon++;
            }
            SelectedWeaponStats();
            EventManager.TriggerEvent("Message", "Switched to " + weaponlist[currentWeapon].name);
        }

        private void SelectedWeaponStats()
        {
            
            selectedWeapon = weaponlist[currentWeapon];
            selectedWeaponStats = selectedWeapon.GetComponent<WeaponStats>();
            remainingbullets = selectedWeaponStats.ClipCapacity;
        }
          
        private IEnumerator ShootBullet()
        {
            able_to_fire = false;

            Rigidbody shellInstance =
                Instantiate(selectedWeaponStats.ammunition.GetComponent<Rigidbody>(), m_FireTransform.position, m_FireTransform.rotation);           
            shellInstance.velocity = selectedWeaponStats.m_MinLaunchForce * m_FireTransform.forward;

            remainingbullets--;
            EventManager.TriggerEvent("Message", string.Format("Firing Weapon. You have {0} bullets.", remainingbullets));
            yield return new WaitForSeconds(selectedWeaponStats.fireRate);
            able_to_fire = true;
        }
    }
}