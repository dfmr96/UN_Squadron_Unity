using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent((typeof(AudioSource)), typeof(Button))]
public class ShopButton : MonoBehaviour, IDeselectHandler, ISelectHandler, ISubmitHandler
{
    public WeaponData weaponData;
    public Inventory playerInventory;
    public Button button;
    public Image subWeaponImage;
    public GameObject mask;
    public AudioSource audioSource;
    public AudioClip[] clip;
    public bool isExitBtn = false;
    public bool isBuyable = true;

    private void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();

        if (weaponData != null)
        {
            subWeaponImage.sprite = weaponData.sprite;
            if (!isExitBtn) CheckInventory();
        }
    }
    public void BuyItem()
    {
        if (GameManager.instance.Money >= weaponData.price)
        {
            GameManager.instance.RemoveMoney(weaponData);
            audioSource.PlayOneShot(clip[1]);
            playerInventory.slots.Add(new Inventory.InventorySlot(weaponData, weaponData.amount));
            mask.SetActive(true);
        }
        else
        {
            Debug.Log("No enought money");
        }
    }

    public void SellItem()
    {
        foreach (Inventory.InventorySlot slot in playerInventory.slots)
        {
            if (slot.weaponData == weaponData)
            {
                playerInventory.slots.Remove(slot);
                break;
            }
        }
        GameManager.instance.AddMoney(weaponData);
        mask.SetActive(false);
    }

    public void CheckInventory()
    {
        foreach (Inventory.InventorySlot slot in playerInventory.slots)
        {
            if (slot.weaponData == weaponData)
            {
                mask.SetActive(true);
            }
        }
    }

    public void FinishPurchase()
    {
        Debug.Log("Has salido de la tienda");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        audioSource.PlayOneShot(clip[0]);
    }

    public void OnSelect(BaseEventData eventData)
    {

    }

    public void OnSubmit(BaseEventData eventData)
    {
        Debug.Log("Money Cheat " + UIStoreManager.instance.moneyCheat);
        if (isBuyable)
        {
            if (!ItemPurchased())
            {
                BuyItem();
            }
            else
            {
                SellItem();
            }
        } else if (isExitBtn)
        {
            LoadingManager.Instance.LoadNewScene("Level1");
        }
        else
        {
            UIStoreManager.instance.moneyCheat++;

            if (UIStoreManager.instance.moneyCheat == 10) 
            {
                GameManager.instance.AddMoney(7000);
            }
            Debug.Log("Cant be bought");
        }
    }

    public bool ItemPurchased()
    {
        foreach (Inventory.InventorySlot slot in playerInventory.slots)
        {
            if (slot.weaponData == weaponData)
            {
                return true;
            }
        }
        return false;
    }
}
