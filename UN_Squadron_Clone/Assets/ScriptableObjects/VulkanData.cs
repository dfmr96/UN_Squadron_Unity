using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [CreateAssetMenu(fileName = "New Vulkan Data", menuName = "Player/VulkanData", order = 0)]
    public class VulkanData : ScriptableObject
    {
        [field:SerializeField] public float VulkanFireRate { get; private set; }
        [field:SerializeField] public int[] VulkanLevels { get; private set; }
        
        [field: SerializeField] public GameObject[] VulkanBullets { get; private set; }
    }
}