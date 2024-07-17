using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.Boss_B2Spirit
{
    public class B2SpiritClusterBomb : MonoBehaviour
    {
        private B2SpiritProjectile cluster;
        [SerializeField] private GameObject clusterShards;
        [SerializeField] private float clusterCookTime;
        [SerializeField] private Transform sideScroll;
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
            StartCoroutine(CookClusters());
        }

        public IEnumerator CookClusters()
        {
            float randomOffset = Random.Range(0f, 1f);
            yield return new WaitForSeconds(clusterCookTime - randomOffset);
            animator.SetBool("canExplode", true);
        }

        public void DestroyBomb()
        {
            Destroy(gameObject);
        }

        public void IgniteCluster()
        {
            Instantiate(clusterShards, transform.position, quaternion.identity);
        }
    }
}
