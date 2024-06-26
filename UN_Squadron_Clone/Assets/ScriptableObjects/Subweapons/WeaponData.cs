using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/New Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int price;
    public float amount;
    [SerializeField] GameObject prefab;
    public Sprite sprite;
    public Sprite weaponSelectorSprite;
    public Sprite weaponNameSprite;
    public Vector3 offsetTransform;

    public void UseWeapon(Transform pTransform)
    {
        Instantiate(prefab, pTransform.position + offsetTransform, Quaternion.identity);
    }
}
