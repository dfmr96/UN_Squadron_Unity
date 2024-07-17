using ScriptableObjects.Enemies.EnemyData;
using UnityEngine;

namespace Enemies.Factory
{
    [CreateAssetMenu(fileName = "AvailableEnemyData", menuName = "Enemy/AvailableEnemyData", order = 0)]
    public class AvailableEnemyData : ScriptableObject
    {
        [SerializeField] public EnemyData[] availableEnemies;
    }
}