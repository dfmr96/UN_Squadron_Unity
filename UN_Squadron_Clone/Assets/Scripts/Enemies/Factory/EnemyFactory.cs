using Enemies.Core;
using ScriptableObjects.Enemies.EnemyData;

namespace Enemies.Factory
{
    public class EnemyFactory : AbstractFactory<Enemy>
    {
        public bool Initialized { get; private set; }
        private EnemyData[] enemyDataArray;
    
        public override Enemy CreateSpawnable(string enemyID)
        {
            foreach (var enemyData in enemyDataArray)
            {
                if (enemyID == enemyData.ID)
                {
                    return enemyData.prefab;
                }
            }
            return default;
        }

        public void Initialize(EnemyData[] enemyList)
        {
            enemyDataArray = enemyList;
            Initialized = true;
        }
    }
}