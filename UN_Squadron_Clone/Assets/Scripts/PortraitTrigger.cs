using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitTrigger : MonoBehaviour
{
    public void ChangeAnimation()
    {
        UIGameplayManager.instance.PlayPortraitRecovery();
    }
}
