using UnityEngine;

namespace Enemies.Patterns
{
    [CreateAssetMenu(fileName = "ZigZagPattern", menuName = "Pattern/ZigZag", order = 2)]
    public class ZigZagPattern : EnemyPattern
    {
        [SerializeField] private float offsetXMultiplier;
        public override Vector3[] GetPattern(int enemyCount)
        {
            Vector3[] positions = new Vector3[enemyCount];

            for (int i = 0; i < enemyCount; i++)
            {
                float y = Mathf.Sin(((Mathf.PI/2)+i*Mathf.PI));
                if (y == 1)
                {
                    OffsetX = 4.5f;
                }
                else
                {
                    OffsetX = 4;
                }
                positions[i] = new Vector3((i * OffsetX) * offsetXMultiplier, y * OffsetY,0f);
            }

            return positions;
        }
    }
}