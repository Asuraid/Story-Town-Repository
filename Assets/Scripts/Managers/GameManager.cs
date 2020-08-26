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
        public int rawGame;
        public int cookedGame;
        [Space(10)]
        public int rawFish;
        public int cookedFish;
        [Space(10)]
        public int rawWood;
        public int refinedWood;
        [Space(10)]
        public int rawStone;
        public int refinedStone;

        [Header("Time")]
        public int maxHours;
        public int maxDay;
        public int hourIncrements = 2;
        [Space(10)]
        public int resetTimerForScene = 1;
        public int timeSkiperAmount;
        [Space(10)]
        public int hours;
        public int currentDay = 1;
        int seasons = 1;

        [Header("Villagers")]
        public List<GameObject> villagerList = new List<GameObject>();
        bool paused;

        public int villagerNumber = 1;
        int eatingCount;

        [Header("Time TextMeshPro")]
        public Sprite[] TimeImages = new Sprite[5];

        public GameObject timeUI;
        SpriteRenderer timeUIrenderer;
        int frames = 0;
        [Space(10)]
        public TextMeshPro textHours;
        public TextMeshPro textDays;
        public TextMeshPro textSeasons;

        [Header("Starvation Warning")]
        public GameObject foodObjectWarning;
        public TextMeshProUGUI foodWarning;

        Camera mainCamera;
        CameraScroller cameraScroller;

        int temporaryStarvationMeter;

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
            // When the game starts, update all UI text.
            // UpdateText();

            // Make sure the frames are at zero.
            frames = 0;

            // Grab camera component.
            mainCamera = Camera.main;

            cameraScroller = mainCamera.GetComponent<CameraScroller>();

            // Grab sprite renderer for time.
            // timeUIrenderer = timeUI.GetComponent<SpriteRenderer>();

            // Add all villagers to list.
            foreach (GameObject villagerObj in GameObject.FindGameObjectsWithTag("Villager"))
            {
                villagerList.Add(villagerObj);
            }

            villagerNumber = villagerList.Count;
        }

        public void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                AddHours();
            }

            if (Input.GetKeyUp(KeyCode.R))
            {
                ResourceManager.Instance.AddRawFish(10);
            }

            if (Input.GetKeyUp(KeyCode.T))
            {
                ResourceManager.Instance.AddCookedFish(10);
            }

            if (Input.GetKeyUp(KeyCode.Y))
            {
                ResourceManager.Instance.AddRawGame(10);
            }

            if (Input.GetKeyUp(KeyCode.U))
            {
                ResourceManager.Instance.AddCookedGame(10);
            }

            if (Input.GetKeyUp(KeyCode.F))
            {
                ResourceManager.Instance.AddRawWood(10);
            }

            if (Input.GetKeyUp(KeyCode.G))
            {
                ResourceManager.Instance.AddRefinedWood(10);
            }

            if (Input.GetKeyUp(KeyCode.H))
            {
                ResourceManager.Instance.AddRawStone(10);
            }
        }

        public void AddHours()
        {
            hours += hourIncrements;

            frames++;

            // Check if the days are more than the max amount of hours. Move onto next day if so. Use up food storage.
            if (hours > maxHours)
            {
                currentDay++;
                hours = 0;
                eatingCount = 0;

                frames = 0;

                print("New day.");

                // If there is enough food, execute this.
                if ((cookedFish + cookedGame) >= villagerNumber)
                {
                    StartCoroutine(FeedVillagers());
                }
                else
                {
                    // Starvation function goes here.
                    print("The villager(s) starved.");
                    temporaryStarvationMeter++;
                    foodObjectWarning.SetActive(true);

                    if (temporaryStarvationMeter >= 2)
                    {
                        foodWarning.text = "Your villagers are very hungry ... it is unlikely they will last one more day.";
                    }

                    if (temporaryStarvationMeter >= 3)
                    {
                        foodWarning.text = "Unfortunately your villagers starved.";
                        StartCoroutine(Countdown(resetTimerForScene));
                    }
                }
            }

            // Change seasons.
            if (currentDay >= maxDay)
            {
                seasons++;
                currentDay = 1;
                Mathf.Clamp(seasons, 0, 4);
            }

            // Update needed UI text.
            UpdateText();

            // Update clock.
            timeUIrenderer.sprite = TimeImages[frames];
        }

        /// <summary>
        /// Pause the villagers, putting their movement to zero.
        /// </summary>
        public void PauseVillagers()
        {
            // Don't run this function if the villagers are already paused.
            if (paused)
                return;
            
            foreach (GameObject villagerObj in villagerList)
            {
                Villager_Prefab villagerScript = null;

                print("running pause");
                if (villagerScript == null)
                    villagerScript = villagerObj.GetComponent<Villager_Prefab>();

                villagerScript.PauseMovement();
            }

            paused = true;
        }

        /// <summary>
        /// Resume the villagers, putting their movement to their norm.
        /// </summary>
        public void ResumeVillagers()
        {
            // Don't run this function if the villagers aren't paused.
            if (!paused)
                return;

            foreach (GameObject villagerObj in villagerList)
            {
                Villager_Prefab villagerScript = null;

                print("running resume");
                if (villagerScript == null)
                    villagerScript = villagerObj.GetComponent<Villager_Prefab>();

                villagerScript.ResumeMovement();
            }

            paused = false;
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
            cameraScroller.enabled = false;
        }

        public void ResumeScrolling()
        {
            cameraScroller.enabled = true;
        }

        void UpdateText()
        {
            textHours.text = "Hours: " + hours + " / " + maxHours;
            textDays.text = "Days: " + currentDay + " / " + maxDay;


            switch (seasons)
            {
                case 1:
                    textSeasons.text = "Season: Spring";
                    break;
                case 2:
                    textSeasons.text = "Season: Summer";
                    break;
                case 3:
                    textSeasons.text = "Season: Fall";
                    break;
                case 4:
                    textSeasons.text = "Season: Winter";
                    break;
                default:
                    print("Something happened to bring it to default.");
                    break;
            }
        }

        private IEnumerator FeedVillagers()
        {
            // If your amount of food is larger than the villagers, consume food.
            while (eatingCount < villagerNumber)
            {
                if (cookedFish > 0)
                {
                    cookedFish--;
                }
                else
                {
                    // Use game if there is no fish.
                    cookedGame--;
                }
                eatingCount++;
            }
            ResourceManager.Instance.cookedFish.GetComponent<Text_Bump>().Bump();
            eatingCount = 0;
            yield return null;
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

