using System;
using DefaultNamespace;
using Player;
using UnityEngine;

namespace ScriptableObjects.Items
{
    [CreateAssetMenu(menuName = "ItemsBehavior/CreateDestroyAllEnemies", fileName = "DestroyAllEnemies", order = 0)]
    public class DestroyAllEnemies : ItemBehavior
    {
        [field:SerializeField] public LayerMask EnemiesMask { get; private set; }
        public override void Use(PlayerController playerController)
        {
            Debug.Log("DestroyAllEnemies Used");
            Bounds mapBounds = playerController.SideScroll.Col.bounds;
            Vector2 pointA = new Vector2(mapBounds.min.x, mapBounds.max.y);
            Vector2 pointB = new Vector2(mapBounds.max.x, mapBounds.min.y);
            Collider2D[] enemiesInside = Physics2D.OverlapAreaAll(pointA, pointB, EnemiesMask);
            Debug.Log(enemiesInside.Length);
            foreach (Collider2D collider in enemiesInside)
            {
                Debug.Log("Enemy detected");
                if (collider.TryGetComponent(out IInstantDestroyable destroyable))
                {
                    destroyable.Destroy();
                }
                
                /*if (collider.TryGetComponent(out Enemy enemy))
                {
                    Debug.Log(enemy.name);
                    enemy.DestroyEnemy();
                }

                if (collider.TryGetComponent(out MiniMissile miniMissile))
                {
                    Destroy(miniMissile);
                }*/
            }
        }
    }
}