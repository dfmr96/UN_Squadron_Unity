using DefaultNamespace;
using UnityEngine;

namespace PowerUps
{
    public abstract class PowerUp : MonoBehaviour, IPickupable
    {
        public abstract void PickUp(PlayerController playerController);
    }
}