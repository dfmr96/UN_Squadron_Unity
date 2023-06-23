using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarTrigger : MonoBehaviour
{
    public void HealthBarToNormal()
    {
        UIManager.instance.HealthBarBackToNormal();
    }
}
