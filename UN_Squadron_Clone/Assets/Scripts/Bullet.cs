using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    private void Update()
    {
        //Solo se mueve hacia la derecha
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Si colisiona con una enemigo, le quita dano y se destruye la bala para no colapsar la memoria
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Si sale de la pantalla, se destruye la bala
        if (collision.gameObject.CompareTag("CameraBounds"))
        {
            Destroy(gameObject);
        }
    }
}
