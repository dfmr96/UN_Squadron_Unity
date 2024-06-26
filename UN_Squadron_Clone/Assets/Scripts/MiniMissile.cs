using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class MiniMissile : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private void Start()
    {
        player = FindAnyObjectByType(typeof(PlayerController)) as PlayerController;
    }

    private void OnEnable()
    {
        EventBus.instance.OnBossDestroyed += DestroyMissiles;
    }

    private void OnDisable()
    {
        EventBus.instance.OnBossDestroyed -= DestroyMissiles;
    }

    private void Update()
    {
        var offset = 0f;
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));

        //transform.LookAt(player.transform.position);
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            player.TakeDamage(damage);
            DestroyMissiles();
        }

        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            DestroyMissiles();
        }
    }

    public void DestroyMissiles()
    {
        Destroy(gameObject);
    }
}
