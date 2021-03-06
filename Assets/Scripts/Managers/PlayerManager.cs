﻿using System;
using UnityEngine;

namespace Complete
{
    [Serializable]
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(WeaponController))]

    public class PlayerManager : LivingEntity
    {
        public delegate void SendMessage(string message);
        public static event SendMessage OnWeaponSwitch;

        public float moveSpeed = 5;
        public Color m_PlayerColor;                             // This is the color this tank will be tinted.
        public Transform m_SpawnPoint;                          // The position and direction the tank will have when it spawns.        
        [HideInInspector] public int m_PlayerNumber;            // This specifies which player this the manager for.        

        private GameObject m_CanvasGameObject;                  // Used to disable the world space UI during the Starting and Ending phases of each round.

        MovementController controller;

        private void Awake()
        {
            controller = GetComponent<MovementController>();
        }

        public override void Start()
        {
         
            
            foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
            {

                renderer.material.color = m_PlayerColor;
            }

            base.Start();
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
        }

    }
}
