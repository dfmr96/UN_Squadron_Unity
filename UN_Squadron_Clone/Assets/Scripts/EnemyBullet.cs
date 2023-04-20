using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    private Vector3 shootDir;

    private void Update()
    {
        transform.Translate(shootDir * speed * Time.deltaTime);
    }

    //La direccion que se setee aca, sera la direccion a la que se movera la bala al spawnear
    public void SetDirection(Vector3 dir)
    {
        shootDir = dir;
    }
}
