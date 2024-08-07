﻿using UnityEngine;

namespace ScriptableObjects.Enemies.EnemySprites
{
    [CreateAssetMenu(fileName = "EnemySprites", menuName = "Enemy/Sprite", order = 0)]
    public class EnemySprites : ScriptableObject
    {
        public Sprite[] enemySprites;
    }
}
