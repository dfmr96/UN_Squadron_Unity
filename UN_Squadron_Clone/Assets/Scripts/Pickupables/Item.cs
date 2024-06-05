using DefaultNamespace;
using UnityEngine;

namespace Pickupables
{
    public abstract class Item : MonoBehaviour, IPickupable
    {
        public abstract void PickUp(PlayerController playerController);
    }
}