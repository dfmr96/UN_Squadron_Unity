using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/New Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] string weaponName;
    public int price;
    public float amount;
    [SerializeField] GameObject prefab;
    public Sprite sprite;
    public Vector3 offsetTransform;

    public void UseWeapon(Transform pTransform)
    {
        Instantiate(prefab, pTransform.position + offsetTransform, Quaternion.identity);
    }
}
