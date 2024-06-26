using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    
    [SerializeField] public GameObject player;
    [SerializeField] private EnemyCommandGenerator enemyCommandGenerator;

    public static SpawnerManager instance;
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
    public void GenerateEnemy(string enemyType, Vector3 spawnPointPosition,GameObject player,bool canDrop)
    {

        if (EnemyPool.ExistEnemyType(enemyType))
        {
            Enemy enemy = EnemyPool.GetEnemy(enemyType);
            if (enemy != null)
            {
                enemy.gameObject.transform.position = spawnPointPosition;
                enemy.gameObject.SetActive(true);
            }

            if (canDrop)
            {
                enemy.CanDrop();
            }
            else
            {
                enemy.CannotDrop();
            }

        }
        else
        {
            if (!enemyCommandGenerator.TryGenerateEnemyCreationCommand(enemyType,
                    spawnPointPosition, new Quaternion(0,180f,0,0),player,canDrop, out var enemyCommand))
            {
                Debug.Log("crea enemigo");
            }
            EventQueue.Instance.EnqueueCommand(enemyCommand);

        }
    }

}
