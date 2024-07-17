using Player;
using UnityEngine;

namespace Enemies.Core
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] int damage;
        [SerializeField] int knockbackForce;
        private Vector3 shootDir;

        private void Update()
        {
            transform.Translate(shootDir * (speed * Time.deltaTime), Space.Self);
        }
        public void SetDirection(Vector3 dir)
        {
            shootDir = dir;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>() != null)
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                Vector3 knowBackDir = (Vector3.left + Vector3.down).normalized;
                rb.AddForce(knowBackDir * knockbackForce, ForceMode2D.Impulse);
                Destroy(gameObject);
            }
        }
    
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("CameraBounds"))
            {
                Destroy(gameObject);
            }
        }
    }
}
