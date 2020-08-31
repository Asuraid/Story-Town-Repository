using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamMars.Capstone.Manager.Resources;
using TMPro;

namespace TeamMars.Capstone.Manager.Resources
{
    public class ResourceGathering : MonoBehaviour
    {
        [Header("Resource Output")]
        [Tooltip("What resource will be gained from this area; can be multiple.")]
        public bool gainFood;

        public bool gainWood;

        public bool gainStone;
        [Space(10)]
        public int gainedAmount;
        [Space(10)]
        public int villagersWorking;
        [Space(20)]
        public TextMeshProUGUI villagers;

        /// <summary>
        /// Update villager text with villagers working.
        /// </summary>
        private void Start()
        {
            villagers.text = "Villagers: " + villagersWorking;
        }

        /// <summary>
        /// On mouse up, add the set amount of resources defined in inspector.
        /// </summary>
        void OnMouseUp()
        {
            AddResource(gainedAmount);
        }

        /// <summary>
        /// Add set amount of resources, check which is added. Does not take into account different gathering amounts for different types.
        /// </summary>
        /// <param name="howmuch">Amount.</param>
        public void AddResource(float howmuch)
        {

            if (gainFood)
            {
                ResourceManager.Instance.AddFood((int)(villagersWorking * howmuch));
            }

            if (gainWood)
            {
                ResourceManager.Instance.AddWood((int)(villagersWorking * howmuch));
            }

            if (gainStone)
            {
                ResourceManager.Instance.AddStone((int)(villagersWorking * howmuch));
            }

        }
    }
}
