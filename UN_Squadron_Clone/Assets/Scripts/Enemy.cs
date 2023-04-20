using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] int health;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate;
    [SerializeField] float fireRateCounter;


    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        fireRateCounter += Time.deltaTime;
        if (fireRateCounter > 1 / fireRate)
        {
            Vector3 aimDir = (player.transform.position - transform.position).normalized;
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetDirection(aimDir);
            fireRateCounter = 0;
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void FireBullet()
    {

    }
}
