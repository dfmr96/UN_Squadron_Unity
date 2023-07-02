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
}
