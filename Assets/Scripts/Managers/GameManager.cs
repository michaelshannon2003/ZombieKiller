using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Complete
{
    public class GameManager : MonoBehaviour
    {
        public int m_NumRoundsToWin = 5;            // The number of rounds a single player has to win to win the game.
        public float m_StartDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases.
        public float m_EndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.
        public CameraControl m_CameraControl;       // Reference to the CameraControl script for control during different phases.
        public GameObject m_ZombieRehab;             // Reference to the prefab the players will control.
        public ZombieManager[] m_zombies;               // A collection of managers for enabling and disabling different aspects of the tanks.

        public GameObject m_PlayerPrefab;             // Reference to the prefab the players will control.
        public PlayerManager[] m_players;               // A collection of managers for enabling and disabling different aspects of the tanks.

        private int m_RoundNumber;                  // Which round the game is currently on.
        private WaitForSeconds m_StartWait;         // Used to have a delay whilst the round starts.
        private WaitForSeconds m_EndWait;           // Used to have a delay whilst the round or game ends.

        public GameObject gameOverPanel;       
        public Text gameplay;


        private void Awake()
        {

          
            m_StartWait = new WaitForSeconds(m_StartDelay);
            m_EndWait = new WaitForSeconds(m_EndDelay);
        }

        private void Start()
        {
            gameOverPanel.SetActive(false);

            SpawnAllPlayers();
            StartCoroutine(GameLoop());
        }

        

        private IEnumerator GameLoop()
        {


            // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
            yield return StartCoroutine(RoundStarting());
            PlayerControl(true);
            ZombieControl(true);
            // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
            yield return StartCoroutine(RoundPlaying());

            if (PlayersRemain())
            {
                //Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
                yield return StartCoroutine(RoundEnding());
                StartCoroutine(GameLoop());
            }
            else
            {
                gameOverPanel.SetActive(true);              
                DestroyZombies();
                PlayerControl(false);
                Canvas.ForceUpdateCanvases();
                gameplay.text =  "Hey !!! " + PlayerNameScoreBoard.displayname + ",  You died";
                EventManager.TriggerEvent("Message", "GameOver");
            }

        }

        private void SpawnAllZombies()
        {

            for (int i = 0; i < m_zombies.Length; i++)
            {
                float x = UnityEngine.Random.Range(-15, 15);
                float z = UnityEngine.Random.Range(-15, 15);
                Vector3 initial_location = new Vector3(x, 1, z);
                m_zombies[i].m_Instance =
                    Instantiate(m_ZombieRehab, initial_location, new Quaternion(0, 0, 0, 0)) as GameObject;
                m_zombies[i].m_ZombieNumber = i + 1;
                m_zombies[i].Setup();               

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


        private void SetCameraTargets()
        {
            // Create a collection of transforms the same size as the number of tanks.
            Transform[] targets = new Transform[m_players.Length];

            // For each of these transforms...
            for (int i = 0; i < targets.Length; i++)
            {
                // ... set it to the appropriate tank transform.
                targets[i] = m_players[i].m_Instance.transform;
            }

            // These are the targets the camera should follow.
            m_CameraControl.m_Targets = targets;
        }


        // This is called from start and will run each phase of the game one after another.
    


        private IEnumerator RoundStarting()
        {
                   
            // As soon as the round starts reset the tanks and make sure they can't move.
            SpawnAllZombies();
            ZombieControl(false);
            PlayerControl(false);
                     
            m_RoundNumber++;
            gameplay.text = "ROUND " + m_RoundNumber;

       
            // Wait for the specified length of time until yielding control back to the game loop.
            yield return m_StartWait;
        }


        private IEnumerator RoundPlaying()
        {
           
            // As soon as the round begins playing let the players control the tanks.          
            // Clear the text from the screen.
            gameplay.text = string.Empty;

            // While there is not one tank left...

            while (ZombiesRemain() && PlayersRemain())
            {
                yield return null;
            }
        }


        private IEnumerator RoundEnding()
        {
            // Stop tanks from moving.
            // DisableTankControl();
            DestroyZombies();
            PlayerControl(false);
            gameplay.text = "ROUND COMPLETE " + m_RoundNumber;
            // Get a message based on the scores and whether or not there is a game winner and display it.        
            //    ClearZombies();
            // Wait for the specified length of time until yielding control back to the game loop.
            yield return m_EndWait;
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

        }
        
    }
}
