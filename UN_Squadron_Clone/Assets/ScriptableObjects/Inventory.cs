using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;

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
