using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossStatus
{
    Healthy,
    Caution,
    Danger
}
public class Boss : MonoBehaviour
{ 
    public float health;
    public float maxHealth;
    public float speed;
    public BossStatus status;
    [SerializeField] Animator bodyAnim;
    [SerializeField] Animator crystalAnim;

    private void Start()
    {
        health = maxHealth;
        status = BossStatus.Healthy;

    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health < maxHealth * 2 / 3)
        {
            status = BossStatus.Caution;
            crystalAnim.SetInteger("Status", (int)status);
        }
        
        if (health < maxHealth * 1 / 3)
        {
            status = BossStatus.Danger;
            crystalAnim.SetInteger("Status", (int)status);
        }

        if(health < 0)
        {
            DestroyEnemy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>() != null)
        {
            TakeDamage(collision.GetComponent<Bullet>().damage);
            Destroy(collision.gameObject);
        }

        if (collision.GetComponent<EnemyEnabler>() != null)
        {
            speed = -1f;
            StartCoroutine(MovingLoop());
        }

    }

    public IEnumerator MovingLoop()
    {
        speed = -1f;
        yield return new WaitForSeconds(4f);
        speed = 3.7f;
        yield return new WaitForSeconds(5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject, 0.5f);
    }
}
