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

        public GameObject m_PlayerPrefab;             // Reference to the prefab the players will control.
        public GameObject map;


        public GameObject gameOverPanel;
        public Text gameplay;

        Bounds mapSize;
        PlayerManager player;



        private void Awake()
        {
            mapSize = map.GetComponent<Renderer>().bounds;
        }

        private void Start()
        {
            gameOverPanel.SetActive(false);


            SpawnAllPlayers();

            player = FindObjectOfType<PlayerManager>();
            player.OnDeath += OnGameOver;
        }

        void Update()
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

        void OnGameOver()
        {
            gameOverPanel.SetActive(true);

            gameplay.text = "Hey !!! " + PlayerNameScoreBoard.displayname + ",  You died";
            EventManager.TriggerEvent("Message", "GameOver");
        }


        private void SpawnZombies(Zombie zombies)
        {

            for (int i = 0; i < zombies.count; i++)
            {
                Instantiate(zombies.m_ZombieRehab, GetRandomSpawnLocation(), new Quaternion(0, 0, 0, 0));
            }
        }

        private void SpawnAllPlayers()
        {
            Instantiate(m_PlayerPrefab, GetRandomSpawnLocation(), new Quaternion(0, 0, 0, 0));
        }

        private Vector3 GetRandomSpawnLocation()
        {

            float xbounds = UnityEngine.Random.Range((mapSize.size.x / 2) * -1, (mapSize.size.x / 2));
            float zbounds = UnityEngine.Random.Range((mapSize.size.z / 2) * -1, (mapSize.size.z / 2));
            return new Vector3(xbounds, 2, zbounds);
        }

        // This is called from start and will run each phase of the game one after another.


        IEnumerator SpawnWave(Wave _wave)
        {
            state = SpawnState.SPAWNING;
            foreach (Zombie zombie in _wave.zombies)
            {
                SpawnZombies(zombie);
            }
            yield return new WaitForSeconds(1f / _wave.rate);
            state = SpawnState.WAITING;
        }


        private void WaveCompleted()
        {
            // Stop tanks from moving.
            // DisableTankControl();
            state = SpawnState.COUNTING;
            waveCountdown = timeBetweenWaves;
            nextWave++;
            totalwavescompleted++;

            gameplay.text = "Wave" + totalwavescompleted + " Completed!";
            if (nextWave == waves.Count())
            {
                nextWave = 0;
            }
        }


        // This is used to check if there is one or fewer tanks remaining and thus the round should end.
        private bool ZombiesRemain()
        {
            if (FindObjectOfType<ZombieManager>() != null)
            { return true; }
            else
            {
                return false;
            }
        }

    }


}

