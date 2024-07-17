using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Player
{
    [Serializable]
    public class Vulkan
    { 
        [SerializeField] private VulkanData vulkanData;
        [SerializeField] private Transform vulkanPosition;
        
        private GameObject currentVulkanBullet => vulkanData.VulkanBullets[currentVulkanLevel];
        private float vulkanFireRate;
        private float vulkanCounter;
        private int currentVulkanPoints;
        private int pointsToNextVulkan;
        private int nextVulkanPoints;
        private int currentVulkanLevel;
        private int[] vulkanLevels;
        
        public void InitVulkan()
        {
            vulkanFireRate = vulkanData.VulkanFireRate;
            vulkanLevels = vulkanData.VulkanLevels;
            vulkanCounter = 0;
            currentVulkanPoints = GameManager.instance.VulkanPoints;
            nextVulkanPoints = CalculateNextVulkanPoints();
            CheckVulkanPoints();
            GameManager.instance.GameplayManager.UpdatePowSprites(nextVulkanPoints, currentVulkanPoints);
            //EventBus.instance.PowTaken(pointsToNextVulkan,currentVulkanPoints);
        }

        private int CalculateNextVulkanPoints()
        {
            return vulkanLevels[currentVulkanLevel + 1] - currentVulkanPoints;
        }
        
        private void FireVulkan()
        {
            Object.Instantiate(currentVulkanBullet, vulkanPosition.position, vulkanPosition.rotation);
            AudioManager.instance.vulkanAudio.Play();
            vulkanCounter = 0;
        }

        public void TryFire()
        {
            if (vulkanCounter > 1 / vulkanFireRate)
            {
                FireVulkan();
            }
        }

        private void UpdateVulkanCounter()
        {
            vulkanCounter += Time.deltaTime;
        }

        public void Update()
        {
            UpdateVulkanCounter();
        }
        
        private void CheckVulkanPoints()
        {
            int pointsExceed = 0;
            if (currentVulkanPoints > nextVulkanPoints)
            {
                pointsExceed = currentVulkanPoints - nextVulkanPoints;
                currentVulkanPoints = nextVulkanPoints;
            }
            if (currentVulkanPoints == nextVulkanPoints)
            {
                currentVulkanLevel++;
            }
            currentVulkanPoints += pointsExceed;
            GameManager.instance.SaveVulkanPoints(currentVulkanPoints);
            nextVulkanPoints = vulkanLevels[currentVulkanLevel + 1];
            pointsToNextVulkan = nextVulkanPoints - currentVulkanPoints;
            EventBus.instance.PowTaken(pointsToNextVulkan,currentVulkanPoints);
            //Debug.Log(pointsToNextVulkan);
        }

        public void AddPoints(int pointToAdd)
        {
            currentVulkanPoints += pointToAdd;
            CheckVulkanPoints();
        }
    }
}