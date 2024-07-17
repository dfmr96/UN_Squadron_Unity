using System;
using System.Collections;
using Interfaces;
using Pickupables;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour, IDamagable
{
    protected bool _customAnim;
    protected SpriteRenderer _spriteRenderer;
    protected Sprite[] _sprites;
    
    
    protected float _health;
    protected int _moveSpeed;
    
    protected bool _canDropPOW;
    [SerializeField] protected SpawneableItems spawneableItems;
    private GameObject itemDropped;
    public GameObject _player;

    protected float _fireRateCounter = 0;
    protected float _fireRate;
    protected GameObject _explosionPrefab;
    protected GameObject _bulletPrefab;

    
    protected float _collisionDamage;

    public EnemyData enemyDataParent;
    
    public int scorePerKill = 100;
    public int moneyPerKill = 300;

    
    protected void DeactiveAllComponents()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        Behaviour[] comps = gameObject.GetComponents<Behaviour>();
        foreach (Behaviour comp in comps)
        {
            comp.enabled = false;
        }
        GetComponent<BoxCollider2D>().enabled = true;
    }



    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(_collisionDamage);
            DestroyEnemy();
        }
    }
    
    public void DestroyEnemy()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        AudioManager.instance.enemyDestroyedAudio.Play();
        EventBus.instance.EnemyDestroyed(this);
        EnemyPool.EnemyDeactivated(this);
        Debug.Log("Enemy Destroyed");
    }
    
    public void TakeDamage(int damage)
    {
        _health -= damage;
        Debug.Log(_health);
        
        if (_health <= 0f)
        {
            if (_canDropPOW && enemyDataParent.canDrop)
            {
                DropItem();
            }
            DestroyEnemy();
            Debug.Log("Enemy Destroy");
        }
        
    }

    protected virtual void Fire()
    {
        if (_player != null && _player.GetComponent<SpriteRenderer>().isVisible)
        {
            
            GameObject bullet = null;
            bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetDirection(AimDirection());
            _fireRateCounter = 0;
        }

    }

    protected Vector3 AimDirection()
    {
        if (_player != null)
        {
            return (_player.transform.position - transform.position).normalized;    
        }

        return new Vector3(0,0,0);
    }

    private void DropItem()
    {
        int index = Random.Range(0, spawneableItems.spawnableItems.Length);
        itemDropped = spawneableItems.spawnableItems[index];
        if (itemDropped == null) return;
        Instantiate(itemDropped, transform.position, Quaternion.identity);
    }

    public void CanDrop()
    {
        _canDropPOW = true;
    }
    public void CannotDrop()
    {
        _canDropPOW = false;
    }
}
