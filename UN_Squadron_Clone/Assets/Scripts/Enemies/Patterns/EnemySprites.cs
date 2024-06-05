using UnityEngine;

namespace Enemies.Patterns
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class EnemySprites : ScriptableObject
    {
        public Sprite[] enemySprites;
    }
}