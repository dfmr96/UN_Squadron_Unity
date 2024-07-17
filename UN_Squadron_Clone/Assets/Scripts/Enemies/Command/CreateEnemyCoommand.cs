using Enemies.Core;
using UnityEngine;

namespace Enemies.Command
{
    public class CreateEnemyCoommand : ICommand
    {
        private Enemy enemyPrefab;
        private Vector3 position;
        private Quaternion rotation;
        private GameObject player;
        private Enemy instance;
        private bool canDrop;

        public CreateEnemyCoommand(Enemy enemyPrefab, Vector3 position, Quaternion rotation, GameObject player, bool canDrop)
        {
            this.enemyPrefab = enemyPrefab;
            this.position = position;
            this.rotation = rotation;
            this.player = player;
            this.canDrop = canDrop;

        }
        public void Execute()
        {
            instance = Object.Instantiate(enemyPrefab, position, rotation);
            instance.GetComponent<Enemy>()._player = player;
            if (canDrop)
            {
                instance.GetComponent<Enemy>().CanDrop();
            }
            EnemyPool.EnemyActivated(instance);
        }
    }
}