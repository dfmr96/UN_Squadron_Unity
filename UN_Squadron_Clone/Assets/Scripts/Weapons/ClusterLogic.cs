using Interfaces;
using UnityEngine;

namespace Weapons
{
    public class ClusterLogic : MonoBehaviour
    {
        [SerializeField] int damage;
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip clip;
        [SerializeField] private LayerMask damagableMask;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(clip);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & damagableMask) != 0)
            {
                Debug.Log($"Detectado {other.gameObject.name}");
                if (other.gameObject.TryGetComponent(out IDamagable damagable))
                {
                    Debug.Log("Dañado");
                    damagable.TakeDamage(damage);
                }
            }
        }

        public void DestroyCluster()
        {
            Destroy(gameObject);
        }
    }
}
