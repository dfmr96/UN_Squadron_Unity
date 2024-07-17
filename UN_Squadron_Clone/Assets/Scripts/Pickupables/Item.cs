using Interfaces;
using Player;
using ScriptableObjects.Items;
using UnityEngine;

namespace Pickupables
{
    public class Item : MonoBehaviour, IPickupable
    {
        [SerializeField] private ItemBehavior[] itemBehaviors;

        public virtual void PickUp(PlayerController playerController)
        {
            foreach (ItemBehavior itemBehavior in itemBehaviors)
            {
                itemBehavior.Use(playerController);
            }
        }
    }
}