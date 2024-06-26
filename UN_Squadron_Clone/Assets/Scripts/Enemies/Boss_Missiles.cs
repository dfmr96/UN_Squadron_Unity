using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEditor.Rendering;
using UnityEngine;

public class Boss_Missiles : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float damage;

    private void OnEnable()
    {
        EventBus.instance.OnBossDestroyed += DestroyMissile;
    }

    private void OnDisable()
    {
        EventBus.instance.OnBossDestroyed -= DestroyMissile;

    }

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            DestroyMissile();
        }
    }

    public void DestroyMissile()
    {
        Destroy(gameObject);
    }
}
