using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] int knockbackForce;
    private Vector3 shootDir;

    private void Start()
    {
        //float angle = Vector3.Angle(transform.right, shootDir);
        //transform.rotation = Quaternion.Euler(0, 0, -angle);
    }

    private void Update()
    {
        transform.Translate(shootDir * speed * Time.deltaTime, Space.Self);
    }

    //La direccion que se setee aca, sera la direccion a la que se movera la bala al spawnear
    public void SetDirection(Vector3 dir)
    {
        shootDir = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 knowBackDir = (Vector3.left + Vector3.down).normalized;
            rb.AddForce(knowBackDir * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
