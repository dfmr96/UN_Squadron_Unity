using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ClusterLogic : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IClusterDamagable clusterDamageable))
        {
            clusterDamageable.TakeDamage(damage);
        }
        
        /*if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }

        if (collision.GetComponent<Boss>() != null)
        {
            collision.GetComponent<Boss>().TakeDamage(damage);
        }

        if (collision.GetComponent<MiniMissile>() != null)
        {
            collision.GetComponent<MiniMissile>().DestroyMissiles();
        }*/
    }



    public void DestroyCluster()
    {
        Destroy(gameObject);
    }
}
