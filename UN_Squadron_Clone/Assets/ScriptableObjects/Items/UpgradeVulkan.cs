using Player;
using UnityEngine;

namespace ScriptableObjects.Items
{
    [CreateAssetMenu(menuName = "ItemsBehavior/Create UpgradeVulkan", fileName = "UpgradeVulkan", order = 0)]
    public class UpgradeVulkan : ItemBehavior
    {
        public int points;
        public override void Use(PlayerController playerController)
        {
            playerController.FrontVulkan.AddPoints(points);
            AudioManager.instance.vulkanPOW.Play();
        }
    }
}