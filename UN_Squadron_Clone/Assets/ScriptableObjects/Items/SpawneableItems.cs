using UnityEngine;

namespace ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "SpawneableItems", menuName = "Items/SpawneableItems", order = 0)]
    public class SpawneableItems : ScriptableObject
    {
        public GameObject[] spawnableItems;
    }
}
