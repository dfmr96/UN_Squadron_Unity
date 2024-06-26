using UnityEngine;

namespace Enemies.Patterns
{
    [CreateAssetMenu(fileName = "DiagonalPattern", menuName = "Pattern/Diagonal", order = 1)]
    public class DiagonalPattern : EnemyPattern
    {
        [SerializeField] private float slope;
        public override Vector3[] GetPattern(int enemyCount)
        {
            Vector3[] positions = new Vector3[enemyCount];

            for (int i = 0; i < enemyCount; i++)
            {
                float y = i * 2f;
                positions[i] = new Vector3(i * OffsetX, y, 0);
            }
            return positions;
        }
    }
}