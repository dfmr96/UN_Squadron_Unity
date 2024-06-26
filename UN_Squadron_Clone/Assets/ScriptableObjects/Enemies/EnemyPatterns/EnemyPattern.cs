using UnityEngine;
using UnityEngine.UIElements;


    [CreateAssetMenu(fileName = "Pattern", menuName = "Enemy/Pattern", order = 0)]
    public abstract class EnemyPattern : ScriptableObject
    {
        [field: SerializeField] public float OffsetX { get; set; }
        [field: SerializeField] public float OffsetY { get; private set; }
        public abstract Vector3[] GetPattern(int enemyCount);
    }
