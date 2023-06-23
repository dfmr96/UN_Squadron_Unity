using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChaseControl : MonoBehaviour
{
    private FlyingEnemy _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<FlyingEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            _enemy.SetTarget(collision.GetComponent<Player>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            _enemy.SetTarget(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius);
    }
}
