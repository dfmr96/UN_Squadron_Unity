using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterLogic : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    public void DestroyCluster()
    {
        Destroy(gameObject);
    }
}
