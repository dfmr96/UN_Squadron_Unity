using Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace ScriptableObjects.Items
{
    public abstract class ItemBehavior : ScriptableObject
    {
        public abstract void Use(PlayerController playerController);
    }
}