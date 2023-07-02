using UnityEngine;

public class SubWeaponController : MonoBehaviour
{
    [SerializeField] Inventory playerInventory;
    //[SerializeField] WeaponData currentWeapon;
    [SerializeField] Inventory.InventorySlot currentSlot;

    private void Start()
    {
        if (playerInventory.slots.Count > 0)
        {
            currentSlot = playerInventory.slots[0];
            EventBus.instance.SubWeaponUsed(currentSlot.amount);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            NextWeapon();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            UseWeapon();
        }
    }

    public void UseWeapon()
    {
        if (currentSlot == null) return;
        if (currentSlot.amount > 0)
        {
            currentSlot.weaponData.UseWeapon(transform);
            EventBus.instance.SubWeaponUsed(currentSlot.amount);
            currentSlot.amount -= 1;
        }

        if (currentSlot.amount <= 0)
        {
            playerInventory.slots.Remove(currentSlot);

            if (playerInventory.slots.Count > 0)
            {
                currentSlot = playerInventory.slots[0];
            }
            else
            {
                currentSlot = null;
            }
            //NextWeapon();
        }
    }

    public void NextWeapon()
    {
        for (int i = 0; i < playerInventory.slots.Count; i++)
        {
            if (currentSlot == playerInventory.slots[i])
            {
                if (i != playerInventory.slots.Count - 1)// Si no es el ultimo
                {
                    currentSlot = playerInventory.slots[i + 1];
                    Debug.Log(currentSlot.weaponData.name + "equipado [if]");
                }
                else //Si es el ultimo
                {
                    currentSlot = playerInventory.slots[0];
                    Debug.Log(currentSlot.weaponData.name + "equipado [else]");
                }
                return;
            }
        }   
    }
}
