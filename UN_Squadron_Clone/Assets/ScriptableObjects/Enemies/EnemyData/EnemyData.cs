
using Enemies.Core;
using UnityEngine;

namespace ScriptableObjects.Enemies.EnemyData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/Data", order = 0)]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public string ID { get; set; }
        [field: SerializeField] public Enemy prefab { get; set; }
        [field: SerializeField] public bool customAnim { get; set; }
        [field: SerializeField] public float fireRate { get; private set; }
        [field: SerializeField] public float collisionDamage { get; private set; }
        [field: SerializeField] public int health { get; private set; }
        [field: SerializeField] public EnemySprites.EnemySprites EnemySprites { get; private set; }
        [field: SerializeField] public GameObject bulletPrefab { get; private set; }
        [field: SerializeField] public GameObject explosionPrefab { get; private set; }
        [field: SerializeField] public int moveSpeed { get; private set; }
    
        [field: SerializeField] public bool canDrop { get; private set; }
    
    }
}
