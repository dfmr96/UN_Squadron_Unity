using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarTrigger : MonoBehaviour
{
    [SerializeField] private UIGameplayManager UIGameplayManager;
    public void HealthBarToNormal()
    {
        UIGameplayManager.HealthBarBackToNormal();
    }
}
