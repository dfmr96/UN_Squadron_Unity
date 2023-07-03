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
            EventBus.instance.SubWeaponChanged(currentSlot.weaponData);
        } else
        {
            EventBus.instance.SubWeaponUsed(0);
            EventBus.instance.SubWeaponChanged(null);
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
            currentSlot.amount -= 1;
            EventBus.instance.SubWeaponUsed(currentSlot.amount);
            Debug.Log(currentSlot.weaponData.weaponName + " gastó un uso");
        }

        if (currentSlot.amount <= 0)
        {
            Debug.Log(currentSlot.weaponData.weaponName + " ya no tiene usos");
            playerInventory.slots.Remove(currentSlot);
            EventBus.instance.SubWeaponChanged(null);
            EventBus.instance.SubWeaponUsed(0);
            Debug.Log(currentSlot.weaponData.weaponName + " ha sido removido del inventario");
            currentSlot = null;
            if (playerInventory.slots.Count > 0)
            {
                currentSlot = playerInventory.slots[0];
                EventBus.instance.SubWeaponChanged(currentSlot.weaponData);
                EventBus.instance.SubWeaponUsed(currentSlot.amount);
                Debug.Log(currentSlot.weaponData.weaponName + " es la nueva arma equipada. Hay al menos un tipo en el inventario");
                //NextWeapon();
            }
            else
            {
                currentSlot = null;
                EventBus.instance.SubWeaponChanged(null);
                Debug.Log("No hay ningun arma en el inventario");
            }
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
                EventBus.instance.SubWeaponChanged(currentSlot.weaponData);
                EventBus.instance.SubWeaponUsed(currentSlot.amount);
                return;
            }
        }   
    }
}
