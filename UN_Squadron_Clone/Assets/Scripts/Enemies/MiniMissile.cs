using Core;
using Player;
using UnityEngine;

namespace Enemies
{
    public enum FollowState
    {
        NotFollowing,
        Following
    }
    public class MiniMissile : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private float angularSpeed;

        [SerializeField] private FollowState followState;
        private void Start()
        {
            player = FindAnyObjectByType(typeof(PlayerController)) as PlayerController;
            SetInitialRotation();
        }

        private void OnEnable()
        {
            EventBus.instance.OnBossDestroyed += DestroyMissiles;
        }

        private void OnDisable()
        {
            EventBus.instance.OnBossDestroyed -= DestroyMissiles;
        }

        private void Update()
        {
            if (followState == FollowState.Following)
            {
                Vector3 direction = GetDirectionToPlayer();
                float horizontalDot = Vector3.Dot(transform.right, direction);
                if (horizontalDot > 0)
                {
                    followState = FollowState.Following;
                }
                else
                {
                    followState = FollowState.NotFollowing;
                }
                transform.rotation = Quaternion.RotateTowards(transform.rotation, GetRotationToPlayer(direction), angularSpeed * Time.deltaTime);
            }
            transform.position += transform.right * (speed * Time.deltaTime);
        }
    
        private void SetInitialRotation()
        {
            var direction = GetDirectionToPlayer();

            var lookRotation = GetRotationToPlayer(direction);

            transform.rotation = lookRotation;
        }

        private Quaternion GetRotationToPlayer(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion lookRotation = Quaternion.Euler(0, 0, angle);
            return lookRotation;
        }

        private Vector3 GetDirectionToPlayer()
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.z = 0; // Ignorar la diferencia en el eje Z
            direction.Normalize();
            return direction;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player.gameObject)
            {
                player.TakeDamage(damage);
                DestroyMissiles();
            }

            if (collision.gameObject.GetComponent<Bullet>() != null)
            {
                DestroyMissiles();
            }
        }
    
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("CameraBounds"))
            {
                DestroyMissiles();
            }
        }

        public void DestroyMissiles()
        {
            Destroy(gameObject);
        }
    }
}