using System;
using System.Collections.Generic;
using ScriptableObjects.Subweapons;
using UnityEngine;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(fileName = "NewInventory", menuName = "New Inventory")]
    public class Inventory : ScriptableObject
    {
        [Serializable]
        public class InventorySlot
        {
            public WeaponData weaponData;
            public float amount;

            public InventorySlot(WeaponData weaponData, float amount)
            {
                this.weaponData = weaponData;
                this.amount = amount;
            }
        }
        public List<InventorySlot> slots;
    }
}
