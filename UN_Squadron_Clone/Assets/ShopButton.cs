using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent((typeof(AudioSource)), typeof(Button))]
public class ShopButton : MonoBehaviour, ISelectHandler
{
    public WeaponData weaponData;
    public Inventory playerInventory;
    public Button button;
    public AudioSource audioSource;
    public AudioClip clip;
    public bool isExitBtn = false;

    private void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        if (!isExitBtn) CheckInventory();
    }
    public void BuyItem()
    {
        playerInventory.slots.Add(new Inventory.InventorySlot(weaponData, weaponData.amount));
        button.interactable = false;
    }

    public void CheckInventory()
    {
        foreach (Inventory.InventorySlot slot in playerInventory.slots)
        {
            if (slot.weaponData == weaponData)
            {
                button.interactable = false;
            }
        }
    }
    public void OnSelect(BaseEventData eventData)
    {
        SubWeaponSelector.OnSubWeaponSelected?.Invoke(this.transform);
        audioSource.Play();
    }

    public void FinishPurchase()
    {
        Debug.Log("Has salido de la tienda");
    }
}
