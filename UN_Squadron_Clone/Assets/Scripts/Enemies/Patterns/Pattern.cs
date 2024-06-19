using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [SerializeField] private EnemyCommandGenerator enemyCommandGenerator;
    [SerializeField] private GameObject player;

    private float x;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 4; i++)
        {
            x = i * 0.75f; 
            GenerateEnemy("Green Helo",this.gameObject.transform.position + new Vector3(i*2,x,0),player);
            GenerateEnemy("Green Helo",this.gameObject.transform.position + new Vector3(i*2,-x,0),player);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
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
