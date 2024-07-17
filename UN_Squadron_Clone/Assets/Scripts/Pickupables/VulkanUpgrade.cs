using Core;
using Player;
using UnityEngine;

namespace Pickupables
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class VulkanUpgrade : Item
    {
        [SerializeField] int points;

        public override void PickUp(PlayerController playerController)
        {
            for (int i = 0; i < playerController.Vulkans.Length; i++)
            {
                playerController.Vulkans[i].AddPoints(points);
            }
            
            AudioManager.instance.vulkanPOW.Play();
        }
    }
}
