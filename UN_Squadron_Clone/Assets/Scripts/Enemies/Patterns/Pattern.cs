using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [SerializeField] private EnemyCommandGenerator enemyCommandGenerator;
    [SerializeField] private GameObject player;

    private float y;

    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 8; i++)
        {
            //Patron Punta
            /*y = i * 0.75f; 
            GenerateEnemy("Green Helo",this.gameObject.transform.position + new Vector3(i*4,y,0),player);
            GenerateEnemy("Green Helo",this.gameObject.transform.position + new Vector3(i*4,-y,0),player);*/

            //Patron Zig Zag
            y = Mathf.Sin(((Mathf.PI/2)+i*Mathf.PI));
            Debug.Log(y);
            if (y == 1)
            {
                offsetX = 4.5f;
            }
            else
            {
                offsetX = 4;
            }
            GenerateEnemy("Green Helo",this.gameObject.transform.position + new Vector3((i+offsetX)*2f,y*offsetY,0),player);
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
