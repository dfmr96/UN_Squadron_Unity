using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnerManager spawnerManager;
    [SerializeField] private EnemyData enemyToSpawn;
    [SerializeField] private EnemyPattern pattern;
    [SerializeField] private int amountToSpawn;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CameraBounds"))
        {
            Vector3[] enemyPosition = pattern.GetPattern(amountToSpawn);
            for (int i = 0; i < amountToSpawn; i++)
            {
                spawnerManager.GenerateEnemy(enemyToSpawn.ID,this.transform.position + enemyPosition[i],spawnerManager.player);
            }
        }
    }


}
