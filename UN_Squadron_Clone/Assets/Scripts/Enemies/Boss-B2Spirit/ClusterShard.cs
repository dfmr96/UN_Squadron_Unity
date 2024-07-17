using Player;
using UnityEngine;

namespace Enemies.Boss_B2Spirit
{
    public class ClusterShard : MonoBehaviour
    {
        [SerializeField] private Vector3 moveDirection;
        [SerializeField] private float speed;
        [SerializeField] private float collisionDamage;
        private Transform parent;
        private void Start()
        {
            parent = transform.parent;
            Destroy(parent.gameObject, 2f);
        }

        private void Update()
        {
            parent.position += moveDirection.normalized * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>() != null)
            {
                other.gameObject.GetComponent<PlayerController>().TakeDamage(collisionDamage);
                Destroy(parent.gameObject);
            }
        }
    }
}
