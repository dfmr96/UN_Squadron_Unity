using ScriptableObjects.Enemies.EnemyData;
using ScriptableObjects.Enemies.EnemyPatterns;
using UnityEngine;

namespace Enemies.Spawner
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnerManager spawnerManager;
        [SerializeField] private EnemyData enemyToSpawn;
        [SerializeField] private EnemyPattern pattern;
        [SerializeField] private int amountToSpawn;

        private void Start()
        {
            spawnerManager = SpawnerManager.instance;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("CameraBounds"))
            {
                Vector3[] enemyPosition = pattern.GetPattern(amountToSpawn);
                for (int i = 0; i < amountToSpawn; i++)
                {
                    if (i == amountToSpawn - 1)
                    {
                        spawnerManager.GenerateEnemy(enemyToSpawn.ID,this.transform.position + enemyPosition[i],spawnerManager.player.gameObject,true);
                    }
                    else
                    {
                        spawnerManager.GenerateEnemy(enemyToSpawn.ID, this.transform.position + enemyPosition[i],
                            spawnerManager.player.gameObject, false);
                    }
                }
            }
        }
    }
}
