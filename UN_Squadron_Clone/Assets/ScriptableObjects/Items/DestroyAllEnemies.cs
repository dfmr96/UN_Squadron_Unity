using System;
using Interfaces;
using Player;
using UnityEngine;

namespace ScriptableObjects.Items
{
    [CreateAssetMenu(menuName = "ItemsBehavior/CreateDestroyAllEnemies", fileName = "DestroyAllEnemies", order = 0)]
    public class DestroyAllEnemies : ItemBehavior
    {
        [field:SerializeField] public LayerMask EnemiesMask { get; private set; }
        [field:SerializeField] public int Damage { get; private set; }
        public override void Use(PlayerController playerController)
        {
            Bounds mapBounds = playerController.SideScroll.Col.bounds;
            Vector2 pointA = new Vector2(mapBounds.min.x, mapBounds.max.y);
            Vector2 pointB = new Vector2(mapBounds.max.x, mapBounds.min.y);
            Collider2D[] enemiesInside = Physics2D.OverlapAreaAll(pointA, pointB, EnemiesMask);
            foreach (Collider2D collider in enemiesInside)
            {
                if (collider.TryGetComponent(out IDamagable damagable))
                {
                    damagable.TakeDamage(Damage);
                }
            }
        }
    }
}