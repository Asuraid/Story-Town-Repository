using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TeamMars.Capstone.Manager.Resources;

namespace TeamMars.Capstone.Manager
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; private set; }

        [Header("Resources")]
        // All persistent variables.
        public int food;
        public int wood;
        public int stone;

        Camera mainCamera;

        // Set game manager up to be persistent.
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {

            // Grab camera component.
            mainCamera = Camera.main;
        }

        public void Update()
        {
            // Shitty shortcuts.
            if (Input.GetKeyUp(KeyCode.W))
            {
                ResourceManager.Instance.AddWood(10);
            }

            if (Input.GetKeyUp(KeyCode.F))
            {
                ResourceManager.Instance.AddFood(10);
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                ResourceManager.Instance.AddStone(10);
            }
        }

        /// <summary>
        /// Pause the entire game.
        /// </summary>
        public void PauseGame()
        {
            Time.timeScale = 0;
            print("Game has been paused!");
        }

        /// <summary>
        /// Unpause the entire game.
        /// </summary>
        public void UnPauseGame()
        {
            Time.timeScale = 1;
            print("Game has been Unpaused!");
        }

        public void StopScrolling()
        {
            //cameraScroller.enabled = false;
        }

        public void ResumeScrolling()
        {
            //cameraScroller.enabled = true;
        }

        private IEnumerator Countdown(int countdown)
        {
            float duration = countdown; // 3 seconds you can change this 
                                        //to whatever you want
            float normalizedTime = 0;
            while (normalizedTime <= 1f)
            {
                normalizedTime += Time.deltaTime / duration;
                yield return null;
            }

            print("reset scene here");
            SceneManager.LoadScene("Scene_Zone02");
        }
    }
}

