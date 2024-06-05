using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "AvailableEnemyData", menuName = "Enemy/AvailableEnemyData", order = 0)]
public class AvailableEnemyData : ScriptableObject
{
    [SerializeField] public EnemyData[] availableEnemies;
}