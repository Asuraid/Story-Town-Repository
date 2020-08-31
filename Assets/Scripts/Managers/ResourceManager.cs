using UnityEngine;
using TMPro;
using System.Collections;

namespace TeamMars.Capstone.Manager.Resources
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance { get; private set; }

        [Header("TextMeshPro References")]
        public TextMeshProUGUI food;
        public TextMeshProUGUI wood;
        public TextMeshProUGUI stone;

        /// <summary>
        /// Set up instance of the game manager. Remove extra instances.
        /// </summary>
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

        /// <summary>
        /// At the beginning, update all text.
        /// </summary>
        void Start()
        {
            UpdateResources();
        }

        #region Gathering resources
        /// <summary>
        /// Add a set amount of food.
        /// </summary>
        /// <param name="amount">Gained amount.</param>
        public void AddFood(int amount)
        {
            GameManager.Instance.food += amount;
            UpdateResources();
        }

        /// <summary>
        /// Add a set amount of wood.
        /// </summary>
        /// <param name="amount">Gained amount.</param>
        public void AddWood(int amount)
        {
            GameManager.Instance.wood += amount;
            UpdateResources();
        }

        /// <summary>
        /// Add a set amount of stone.
        /// </summary>
        /// <param name="amount">Gained amount.</param>
        public void AddStone(int amount)
        {
            GameManager.Instance.stone += amount;
            UpdateResources();
        }
        #endregion

        #region Using resources
        /// <summary>
        /// Remove a set amount of food.
        /// </summary>
        /// <param name="amount">Gained amount.</param>
        public void RemoveFood(int amount)
        {
            GameManager.Instance.food -= amount;
            UpdateResources();
        }

        /// <summary>
        /// Remove a set amount of wood.
        /// </summary>
        /// <param name="amount">Gained amount.</param>
        public void RemoveWood(int amount)
        {
            GameManager.Instance.wood -= amount;
            UpdateResources();
        }

        /// <summary>
        /// Remove a set amount of stone.
        /// </summary>
        /// <param name="amount">Gained amount.</param>
        public void RemoveStone(int amount)
        {
            GameManager.Instance.stone -= amount;
            UpdateResources();
        }
        #endregion

        /// <summary>
        /// Update all resource texts using the variables located in the Game Manager.
        /// </summary>
        public void UpdateResources()
        {
            food.text = "Food: " + GameManager.Instance.food.ToString();
            wood.text = "Wood: " + GameManager.Instance.wood.ToString();
            stone.text = "Stone: " + GameManager.Instance.stone.ToString();
        }
    }
}

