using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class B2Spirit : MonoBehaviour, IDamagable
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform bombSpawn;
    [SerializeField] private GameObject clusterBombPrefab;
    [SerializeField] private float clusterBombCooldown;
    [SerializeField] private Transform[] clusterBombSpawn;
    [SerializeField] private GameObject burstPrefab;
    [SerializeField] private bool canFireClusters;
    [SerializeField] private int burstShootQuantity;
    [SerializeField] private float timeBetweenBurstShoots;
    [SerializeField] private float burstCooldown;
    [SerializeField] private bool canShootBurst;
    [SerializeField] private Transform burstSpawn;
    [SerializeField] private Animator animator;
    [SerializeField] private bool entryFinished;
    [SerializeField] private GameObject[] triggers;
    [SerializeField] private Collider2D col;
    [SerializeField] private GameObject flames;
    [SerializeField] private GameObject destroyedFX;

    private void Start()
    {
        canShootBurst = true;
        canFireClusters = true;
        health = maxHealth;
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= maxHealth / 2 && !flames.activeSelf)
        {
            flames.SetActive(true);
            clusterBombCooldown *= 0.33f;
        }
        
        if (health <= 0)
        {
            DestroyBoss();
        }
    }

    public void DropBomb()
    {
        Instantiate(bombPrefab, bombSpawn);
    }

    public void FireBackwardsBurst()
    {
        if (!canShootBurst || !entryFinished) return;
        StartCoroutine(FireBurst());
    }

    private IEnumerator FireBurst()
    {
        animator.SetBool("canFireBurst", canShootBurst);
        canShootBurst = false;
        for (int i = 0; i < burstShootQuantity; i++)
        {
            Instantiate(burstPrefab, burstSpawn);
            yield return new WaitForSeconds(timeBetweenBurstShoots);
        }
        animator.SetBool("canFireBurst", canShootBurst);
        yield return new WaitForSeconds(burstCooldown);
        canShootBurst = true;
    }

    private void FireClusterBombs()
    {
        for (int i = 0; i < clusterBombSpawn.Length; i++)
        {
            Instantiate(clusterBombPrefab, clusterBombSpawn[i]);
        }
        animator.SetBool("canFireCluster", false);
    }

    private IEnumerator ClusterBombs_CO()
    {
        yield return new WaitForSeconds(clusterBombCooldown);
        animator.SetBool("canFireCluster", true);
        StartCoroutine(ClusterBombs_CO());
    }

    private void FinishEntry()
    {
        entryFinished = true;
        foreach (GameObject trigger in triggers)
        {
            trigger.SetActive(true);
        }

        col.enabled = true;
        animator.SetBool("entryFinished", entryFinished);
        StartCoroutine(ClusterBombs_CO());
    }

    private void DestroyBoss()
    {
        destroyedFX.SetActive(true);
        col.enabled = false;
        canShootBurst = false;
        canFireClusters = false;
        foreach (GameObject trigger in triggers)
        {
            trigger.SetActive(false);
        }
        animator.SetBool("isDestroyed", true);
    }
}
