using DefaultNamespace;
using UnityEngine;

namespace Pickupables
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class VulkanUpgrade : MonoBehaviour, IPickupable
    {
        [SerializeField] int points;

        public void PickUp(PlayerController playerController)
        {
            playerController.FrontVulkan.AddPoints(points);
            AudioManager.instance.vulkanPOW.Play();
        }
    }
}
