using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Complete
{
    public class GameManager : MonoBehaviour
    {
        public enum SpawnState { SPAWNING, WAITING, COUNTING };

        [System.Serializable]
        public class Zombie
        {
            public int count;
            public GameObject m_ZombieRehab;

        }

        [System.Serializable]
        public class Wave
        {
            
            public float rate;
            public Zombie[] zombies;
        }

        public Wave[] waves;

        private SpawnState state = SpawnState.COUNTING;
        public SpawnState State
        {
            get { return state; }
        }

        private int nextWave = 0;
        private int totalwavescompleted;
        public float timeBetweenWaves = 5f;
        private float waveCountdown;      


        private List<ZombieManager> m_zombies = new List<ZombieManager>();               // A collection of managers for enabling and disabling different aspects of the tanks.
        public GameObject m_PlayerPrefab;             // Reference to the prefab the players will control.
               
        public PlayerManager[] m_players;               // A collection of managers for enabling and disabling different aspects of the tanks.


        public GameObject gameOverPanel;
        public Text gameplay;


        private void Awake()
        {
        }

        private void Start()
        {
            gameOverPanel.SetActive(false);

            SpawnAllPlayers();
        }

        void Update()
        {

            if (PlayersRemain())
            {
            
                if (state == SpawnState.WAITING)
                {
                    if (!ZombiesRemain())
                    {
                        WaveCompleted();
                    }

                }
                else
                {

                    if (waveCountdown <= 0)
                    {
                        if (state != SpawnState.SPAWNING)
                        {
                            gameplay.text = "";
                            StartCoroutine(SpawnWave(waves[nextWave]));
                        }
                    }
                    else
                    {
                        waveCountdown -= Time.deltaTime;
                    
                    }
                }
            }
            else
            {

                gameOverPanel.SetActive(true);
                DestroyZombies();
                PlayerControl(false);

                gameplay.text = "Hey !!! " + PlayerNameScoreBoard.displayname + ",  You died";
                EventManager.TriggerEvent("Message", "GameOver");
            }

        }


        private void SpawnZombies(Zombie zombies)
        {

            for (int i = 0; i < zombies.count; i++)
            {
                float x = UnityEngine.Random.Range(-15, 15);
                float z = UnityEngine.Random.Range(-15, 15);
                Vector3 initial_location = new Vector3(x, 1, z);
                ZombieManager zombie = new ZombieManager()
                {
                    m_Instance = Instantiate(zombies.m_ZombieRehab, initial_location, new Quaternion(0, 0, 0, 0)) as GameObject,
                    m_ZombieNumber = i + 1,
                };

                zombie.Setup();
                m_zombies.Add(zombie);

            }
        }

        private void SpawnAllPlayers()
        {
            // For all the tanks...
            for (int i = 0; i < m_players.Length; i++)
            {
                // ... create them, set their player number and references needed for control.
                m_players[i].m_Instance =
                    Instantiate(m_PlayerPrefab, m_players[i].m_SpawnPoint.position, m_players[i].m_SpawnPoint.rotation) as GameObject;
                m_players[i].m_PlayerNumber = i + 1;
                m_players[i].Setup();
            }
        }

        // This is called from start and will run each phase of the game one after another.


        IEnumerator SpawnWave(Wave _wave)
        {
            state = SpawnState.SPAWNING;
            foreach (Zombie zombie in _wave.zombies) {
                SpawnZombies(zombie);
            }
            yield return new WaitForSeconds(1f / _wave.rate);
            state = SpawnState.WAITING;
        }


        private void WaveCompleted()
        {
            // Stop tanks from moving.
            // DisableTankControl();
            DestroyZombies();
            state = SpawnState.COUNTING;
            waveCountdown = timeBetweenWaves;
            nextWave++;
            totalwavescompleted++;

            gameplay.text = "Wave" + totalwavescompleted + " Completed!";
            if (nextWave == waves.Count()) {
                nextWave = 0;
                    }
        }


        // This is used to check if there is one or fewer tanks remaining and thus the round should end.
        private bool ZombiesRemain()
        {
            if (m_zombies.Any(zombie => zombie.m_Instance.GetComponent<Health>().m_Dead == false))
            { return true; }
            else
            {
                return false;
            }

        }

        private bool PlayersRemain()
        {

            if (m_players.Any(player => player.m_Instance.GetComponent<Health>().m_Dead == false))
            { return true; }
            else
            {
                return false;
            }
        }

        // This function is to find out if there is a winner of the round.
        // This function is called with the assumption that 1 or fewer tanks are currently active.

        private void PlayerControl(bool state)
        {
            foreach (PlayerManager player in m_players)
            {
                player.EnableControl(state);
            }
        }

        private void ZombieControl(bool state)
        {
            foreach (ZombieManager zombie in m_zombies)
            {
                zombie.EnableControl(state);
            }
        }

        private void DestroyPlayers()
        {
            foreach (PlayerManager player in m_players)
            {
                Destroy(player.m_Instance);
            }

        }

        private void DestroyZombies()
        {
            foreach (ZombieManager zombie in m_zombies)
            {
                Destroy(zombie.m_Instance);
            }
            m_zombies.Clear();

        }

    }


}
