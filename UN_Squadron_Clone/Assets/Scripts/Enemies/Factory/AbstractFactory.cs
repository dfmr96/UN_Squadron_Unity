public abstract class AbstractFactory<T> where T: Enemy
{
    public abstract T CreateSpawnable(string enemyID);
}