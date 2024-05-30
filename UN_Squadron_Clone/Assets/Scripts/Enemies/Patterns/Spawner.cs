using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public Transform[] spawnPoints;

    [SerializeField] public Enemy[] enemyPrefab;

    [SerializeField] public int amountToSpawn;
    [SerializeField] public float xOffSet;
    [SerializeField] public float yOffSet;

    [SerializeField] private GameObject player;
    
    
    [SerializeField] private EnemyCommandGenerator enemyCommandGenerator;


    private string enemyType;
    enum EnemyType
    {
        GreenHelo,
        OrangeHelo,
        Tank,
        DefaultTurret
    }
    
    [SerializeField] private EnemyType EnemyID;
    private void Awake()
    {
        switch (EnemyID)
        {
            case EnemyType.GreenHelo:
                enemyType = "Green Helo";
                break;
            case EnemyType.OrangeHelo:
                enemyType = "Orange Helo";
                break;
            case EnemyType.Tank:
                enemyType = "Tank";
                break;
            case EnemyType.DefaultTurret:
                enemyType = "Default Turret";
                break;
            default:
                enemyType = "Green Helo";
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            for (int i = 0; i < amountToSpawn; i++)
            {
                GenerateEnemy(enemyType,spawnPoint.position + new Vector3(xOffSet*i, yOffSet*i, 0),player);
            }   
        }
    }
    
    private void GenerateEnemy(string enemyType, Vector3 spawnPointPosition,GameObject player)
    {
        if (!enemyCommandGenerator.TryGenerateEnemyCreationCommand(enemyType,
                spawnPointPosition, new Quaternion(0,180f,0,0),player, out var enemyCommand))
        {
            return;
        }
            
        EventQueue.Instance.EnqueueCommand(enemyCommand);
    }

}
