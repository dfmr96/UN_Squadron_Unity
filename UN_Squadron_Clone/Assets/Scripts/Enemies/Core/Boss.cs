using System;
using System.Collections;
using UnityEngine;

public enum BossStatus
{
    Healthy,
    Caution,
    Danger,
    Destroyed
}
public class Boss : Enemy
{
    public float health;
    public float maxHealth;
    public float speed;
    public float fireRate;
    public float fireTimer;
    public GameObject bodyDamaged;
    public BossStatus status;
    [SerializeField] Animator bodyAnim;
    [SerializeField] Animator crystalAnim;
    [SerializeField] GameObject missilesPrefab;
    [SerializeField] GameObject miniMisilesPrefab;
    [SerializeField] GameObject flames;
    [SerializeField] Transform[] miniMisilesPos;
    [SerializeField] Transform missiles;
    [SerializeField] bool canFire = false;

    [SerializeField] private EnemyData _enemyData;

    [SerializeField] private int BossLevel;

    private void Awake()
    {
        _explosionPrefab = _enemyData.explosionPrefab;
    }

    private void Start()
    {
        health = maxHealth;
        status = BossStatus.Healthy;

    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        fireTimer += Time.deltaTime;
        if (fireTimer > 1 / fireRate && canFire)
        {
            Instantiate(missilesPrefab, missiles.position, Quaternion.identity);
            for (int i = 0; i < miniMisilesPos.Length; i++)
            {
                Instantiate(miniMisilesPrefab, miniMisilesPos[i].position, Quaternion.identity);
            }
            AudioManager.instance.bossMisiles.Play();
            fireTimer = 0;
        }
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
            bodyDamaged.SetActive(true);
        }

        if (health < 0)
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
        canFire = true;

        while (status != BossStatus.Destroyed)
        {
            speed = -1f;
            yield return new WaitForSeconds(4.5f);
            speed = 3.7f;
            yield return new WaitForSeconds(5f);
            speed = 9f;
            yield return new WaitForSeconds(4f);
        }

    }

    private void DestroyEnemy()
    {
        canFire = false;
        status = BossStatus.Destroyed;
        StopCoroutine(MovingLoop() );
        speed = 0f;
        bodyAnim.SetBool("Destroyed", true);
        crystalAnim.gameObject.SetActive(false);
        flames.SetActive(true);
        AudioManager.instance.bossDestroyed.Play();
        //AudioManager.instance.bossBGM.Stop();
        //Destroy(gameObject, 0.5f);
        GameManager.instance.SetLevelCompleted(BossLevel);
        EventBus.instance.BossDestroyed();
        //Pausar gameplay
        //Desahablitar InputJugador
        //Correr video de cuenta atras
        //Ir a ventana de victoria
    }


    public void Fire(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }
}
