using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/New Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] string weaponName;
    [SerializeField] float price;
    public float amount;
    [SerializeField] GameObject prefab;
    [SerializeField] Sprite sprite;
}
