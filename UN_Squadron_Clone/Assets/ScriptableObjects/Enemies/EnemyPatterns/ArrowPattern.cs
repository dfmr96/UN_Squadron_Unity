using UnityEngine;

namespace Enemies.Patterns
{
    [CreateAssetMenu(fileName = "ArrowPattern", menuName = "Pattern/Arrow", order = 0)]
    public class ArrowPattern : EnemyPattern
    {
        public override Vector3[] GetPattern(int enemyCount)
        {
            Vector3[] positions = new Vector3[enemyCount];

            for (int i = 1; i < enemyCount; i +=2)
            {
                float y = i * 0.75f;
                positions[i] = new Vector3(i * OffsetX, y, 0);
                positions[i + 1] = new Vector3(i * OffsetX, -y, 0);

            }
            /*GenerateEnemy(EnemyToSpawn.ID,this.gameObject.transform.position + new Vector3(i*offsetX,y,0),player);
            GenerateEnemy(EnemyToSpawn.ID,this.gameObject.transform.position + new Vector3(i*offsetX,-y,0),player);*/

            return positions;
        }
    }
}