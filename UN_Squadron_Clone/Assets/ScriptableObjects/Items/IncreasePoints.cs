using Player;
using UnityEngine;

namespace ScriptableObjects.Items
{
    [CreateAssetMenu(menuName = "ItemsBehavior/Create IncreasePoints", fileName = "IncreasePoints", order = 0)]
    public class IncreasePoints : ItemBehavior
    {
        [field:SerializeField] public int Score { get; private set; }
        public override void Use(PlayerController playerController)
        {
            GameManager.instance.UpdateScore(Score);
        }
    }
}