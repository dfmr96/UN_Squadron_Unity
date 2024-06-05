using DefaultNamespace;
using UnityEngine;

namespace Pickupables
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class VulkanUpgrade : Item
    {
        [SerializeField] int points;

        public override void PickUp(PlayerController playerController)
        {
            playerController.FrontVulkan.AddPoints(points);
            AudioManager.instance.vulkanPOW.Play();
        }
    }
}
