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
    public void SetDirection(Vector3 dir)
    {
        shootDir = dir;
    }
}
