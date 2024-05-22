using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy", order = 0)]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public bool _customAnim { get; set; }
    [field: SerializeField] public float _fireRate { get; private set; }
    [field: SerializeField] public float _collisionDamage { get; private set; }
    [field: SerializeField] public int _health { get; private set; }
}
