public class EnemyFactory : AbstractFactory<Enemy>
{
    public bool Initialized { get; private set; }
    
    private EnemyData[] enemyDataArray;
    
    public override Enemy CreateSpawnable(string enemyID)
    {
        
        foreach (var enemyData in enemyDataArray)
        {
            if (enemyData.ID == enemyID)
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