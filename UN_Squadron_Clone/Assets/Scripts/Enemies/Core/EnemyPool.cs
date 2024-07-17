using System.Collections.Generic;

namespace Enemies.Core
{
    public static class EnemyPool 
    {
        public static List<Enemy> activeList = new List<Enemy>();
        public static List<Enemy> notActiveList = new List<Enemy>();
    
        public static Enemy GetEnemy(string enemyType)
        {
            if (notActiveList.Count > 0)
            {
                foreach (Enemy notActiveEnemy in notActiveList)
                {
                    if (notActiveEnemy.enemyDataParent.ID == enemyType)
                    {
                        Enemy enemy = notActiveEnemy;
                        notActiveList.Remove(notActiveEnemy);
                        activeList.Add(enemy);
                        return enemy;
                    }
                }
            }
            return null;
        }

        public static void EnemyDeactivated(Enemy enemyNotActive)
        {
            activeList.Remove(enemyNotActive);
            notActiveList.Add(enemyNotActive);
            enemyNotActive.gameObject.SetActive(false);
        }

        public static void EnemyActivated(Enemy activeEnemy)
        {
            activeList.Add(activeEnemy);
        }
        public static bool ExistEnemyType(string enemyType)
        {
            foreach (Enemy notActiveEnemy in notActiveList)
            {
                if (enemyType == notActiveEnemy.enemyDataParent.ID)
                {
                    return true;
                }
            }

            return false;
        }

        public static void ClearPool()
        {
            activeList.Clear();
            notActiveList.Clear();
        }

        public static void EnemyDestroyed(Enemy destroyedEnemy)
        {
            if (activeList.Contains(destroyedEnemy))
            {
                activeList.Remove(destroyedEnemy);
            }else if (notActiveList.Contains(destroyedEnemy))
            {
                notActiveList.Remove(destroyedEnemy);
            }
        }
    }
}