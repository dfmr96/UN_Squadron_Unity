using Enemies.Core;

namespace Enemies.Factory
{
    public abstract class AbstractFactory<T> where T: Enemy
    {
        public abstract T CreateSpawnable(string enemyID);
    }
}