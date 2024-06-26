using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] public GameObject player;
    [SerializeField] private EnemyCommandGenerator enemyCommandGenerator;

    public static Spawner instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    public void GenerateEnemy(string enemyType, Vector3 spawnPointPosition,GameObject player)
    {

        if (EnemyPool.ExistEnemyType(enemyType))
        {
            var enemy = EnemyPool.GetEnemy(enemyType);
            enemy.gameObject.transform.position = spawnPointPosition;
            enemy.gameObject.SetActive(true);
        }
        else
        {
            if (!enemyCommandGenerator.TryGenerateEnemyCreationCommand(enemyType,
                    spawnPointPosition, new Quaternion(0,180f,0,0),player, out var enemyCommand))
            {
                Debug.Log("crea enemigo");
                return;
            }
            EventQueue.Instance.EnqueueCommand(enemyCommand);
        }
    }

}
