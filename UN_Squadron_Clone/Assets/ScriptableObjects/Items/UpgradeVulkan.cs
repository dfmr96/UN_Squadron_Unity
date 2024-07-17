using Core;
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
            foreach (var t in playerController.Vulkans)
            {
                t.AddPoints(points);
            }
            AudioManager.instance.vulkanPOW.Play();
        }
    }
}