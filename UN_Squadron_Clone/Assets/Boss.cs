using System.Collections;
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
    public float fireRate;
    public float fireTimer;
    public GameObject bodyDamaged;
    public BossStatus status;
    [SerializeField] Animator bodyAnim;
    [SerializeField] Animator crystalAnim;
    [SerializeField] GameObject missilesPrefab;
    [SerializeField] GameObject miniMisilesPrefab;
    [SerializeField] Transform[] miniMisilesPos;
    [SerializeField] Transform missiles;
    [SerializeField] bool canFire = false;

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
            AudioManager.instance.boosMisiles.Play();
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

        while (gameObject.activeSelf)
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
        Destroy(gameObject, 0.5f);
    }
}
