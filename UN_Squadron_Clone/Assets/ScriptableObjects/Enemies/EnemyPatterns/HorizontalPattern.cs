using UnityEngine;

namespace ScriptableObjects.Enemies.EnemyPatterns
{
    [CreateAssetMenu(fileName = "HorizontalPattern", menuName = "Pattern/Horizontal", order = 3)]
    public class HorizontalPattern : EnemyPattern
    {
        public override Vector3[] GetPattern(int enemyCount)
        {
            Vector3[] positions = new Vector3[enemyCount];
            for (int i = 0; i < enemyCount; i++)
            {
                positions[i] = new Vector3((i * OffsetX), 0f,0f);
            }
            return positions;
        }
    }
}