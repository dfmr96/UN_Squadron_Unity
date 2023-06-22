using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public static EventBus instance;

    public event Action<Enemy> OnEnemyDestroyed;



    // Start is called before the first frame update
    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            Debug.Log("EventBus Instance Created");
        } else
        {
            Destroy(gameObject);
        }
    }

    public void EnemyDestroyed(Enemy enemy)
    {
        OnEnemyDestroyed?.Invoke(enemy);
    }
}
