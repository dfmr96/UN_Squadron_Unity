using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    public int damage;
    private void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) //TODO
        {
            collision.gameObject.TryGetComponent(out IDamageable enemy);
            enemy.TakeDamage(damage);
            Debug.Log("Bala colisiono con enemigo");
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacles")) Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CameraBounds"))
        {
            Destroy(gameObject);
        }
    }
}
