using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeaponSelector : MonoBehaviour
{
    public static Action<Transform> OnSubWeaponSelected;

    private void OnEnable()
    {
        OnSubWeaponSelected += SubweaponSelected;
    }

    private void OnDisable()
    {
        OnSubWeaponSelected -= SubweaponSelected;
    }

    public void SubweaponSelected(Transform subWeaponTransform)
    {
        this.transform.position = subWeaponTransform.position;
    }
}
