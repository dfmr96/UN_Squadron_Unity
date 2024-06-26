using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewFactoryInitializer", menuName = "Factory/EnemyFactoryInitializer", order = 0)]
public class EnemyFactoryInitializer : ScriptableObject
{
    private EnemyFactory enemyFactory = new();

    [SerializeField] private AvailableEnemyData availableEnemiesData;

    public Enemy GetEnemy(string enemyID)
    {
        if (!enemyFactory.Initialized)
        {
            foreach (var enemyData in availableEnemiesData.availableEnemies)
            {
                //if (enemyID == enemyData.ID)
                //{
                    enemyFactory.Initialize(availableEnemiesData.availableEnemies);
                    //break;
                //}
            }
        }

        return enemyFactory.CreateSpawnable(enemyID);
    }

}