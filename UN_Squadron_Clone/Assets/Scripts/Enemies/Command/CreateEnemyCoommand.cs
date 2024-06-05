using UnityEngine;

public class CreateEnemyCoommand : ICommand
{
    private Enemy enemyPrefab;
    private Vector3 position;
    private Quaternion rotation;
    private GameObject player;
    private Enemy instance;

    public CreateEnemyCoommand(Enemy enemyPrefab, Vector3 position, Quaternion rotation, GameObject player)
    {
        this.enemyPrefab = enemyPrefab;
        this.position = position;
        this.rotation = rotation;
        this.player = player;

    }
    public void Execute()
    {
        instance = Object.Instantiate(enemyPrefab, position, rotation);
        instance.GetComponent<Enemy>()._player = player;
    }
}