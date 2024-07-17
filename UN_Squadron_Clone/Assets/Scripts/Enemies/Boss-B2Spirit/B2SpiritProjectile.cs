using Player;
using UnityEngine;

namespace Enemies.Boss_B2Spirit
{
    public class B2SpiritProjectile : MonoBehaviour
    {
        [SerializeField] private Vector3 direction;
        [SerializeField] private float speed;
        [SerializeField] private float damage;

        private void Start()
        {
            Destroy(gameObject, 3f);
        }

        private void Update()
        {
            transform.position += direction * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>() != null)
            {
                other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }
    }
}
