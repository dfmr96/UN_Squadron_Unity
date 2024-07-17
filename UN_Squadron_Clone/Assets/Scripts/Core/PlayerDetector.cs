using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private UnityEvent events;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            events.Invoke();
        }
    }
}
