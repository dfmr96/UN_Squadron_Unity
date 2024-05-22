using System;
using UnityEngine;

namespace Player
{
    public class Vulkan : MonoBehaviour
    {
        [SerializeField] private VulkanData vulkanData;
        
        private GameObject currentVulkanBullet => vulkanData.VulkanBullets[currentVulkanLevel];
        private float vulkanFireRate;
        private float vulkanCounter;
        private int currentVulkan;
        
        private int pointsToNextVulkan;
        private int nextVulkanPoints;
        private int currentVulkanLevel;
        private int[] vulkanLevels;

        private void Start()
        {
            InitVulkan();
        }

        public void InitVulkan()
        {
            vulkanFireRate = vulkanData.VulkanFireRate;
            vulkanLevels = vulkanData.VulkanLevels;
            
            
            
            nextVulkanPoints = CalculateNextVulkanPoints();
            
        }

        private int CalculateNextVulkanPoints()
        {
            return vulkanLevels[currentVulkanLevel + 1] - currentVulkan;
        }
        
        private void FireVulkan()
        {
            Instantiate(currentVulkanBullet, transform.position, Quaternion.identity);
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

        private void Update()
        {
            UpdateVulkanCounter();
        }
        
        private void CheckVulkanPoints()
        {
            int pointsExceed = 0;
            if (currentVulkan > nextVulkanPoints)
            {
                pointsExceed = currentVulkan - nextVulkanPoints;
                currentVulkan = nextVulkanPoints;
            }
            if (currentVulkan == nextVulkanPoints)
            {
                currentVulkanLevel++;
            }
            currentVulkan += pointsExceed;
            nextVulkanPoints = vulkanLevels[currentVulkanLevel + 1];
            pointsToNextVulkan = nextVulkanPoints - currentVulkan;
        }
    }
}