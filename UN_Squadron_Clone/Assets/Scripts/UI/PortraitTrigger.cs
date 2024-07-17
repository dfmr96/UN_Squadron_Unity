using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitTrigger : MonoBehaviour
{
    [SerializeField] private UIGameplayManager UIGameplayManager;
    public void ChangeAnimation()
    {
        UIGameplayManager.PlayPortraitRecovery();
    }
}
