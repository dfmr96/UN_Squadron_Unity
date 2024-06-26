﻿using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCommandFactory", menuName = "EnemyCommandFactory", order = 0)]
public class EnemyCommandGenerator : ScriptableObject
{
    [SerializeField] public EnemyFactoryInitializer enemyFactoryInitializer;

    public bool TryGenerateEnemyCreationCommand(string enemyType, Vector3 position,Quaternion rotation,GameObject player,bool CanDrop, out ICommand command)
    {
        var enemy = enemyFactoryInitializer.GetEnemy(enemyType);
        
        command = new CreateEnemyCoommand(enemy, position, rotation,player,CanDrop);
        return command != null;
    }
}