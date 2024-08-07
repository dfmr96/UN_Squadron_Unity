using Core;
using Player;
using UnityEngine;

namespace Enemies.Core
{
    public class BossMissiles : MonoBehaviour
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
            transform.Translate(Vector3.left * (speed * Time.deltaTime));
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>() != null)
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
                DestroyMissile();
            }
        }
    
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("CameraBounds"))
            {
                Destroy(gameObject);
            }
        }

        private void DestroyMissile()
        {
            Destroy(gameObject);
        }
    }
}
