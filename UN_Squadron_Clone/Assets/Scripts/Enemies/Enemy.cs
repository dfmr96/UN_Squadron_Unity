using System;
using System.Collections;
using Player;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour, IDamageable
{
    protected bool _customAnim;
    protected SpriteRenderer _spriteRenderer;
    protected Sprite[] _sprites;
    
    
    protected float _health;
    protected int _moveSpeed;
    
    protected bool _canDropPOW;
    //protected VulkanPOWType _type; TODO
    [SerializeField] VulkanPOWs _vulkanPOWsGO;
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
        Destroy(gameObject);
        Debug.Log("Enemy Destroyed");
    }
    
    public void TakeDamage(int damage)
    {
        _health -= damage;
        Debug.Log(_health);
        
        if (_health <= 0f)
        {            
            DropItem();
           // EnemyPool.EnemyDestroyed(this);
           // DestroyEnemy();
           EnemyPool.EnemyDeactivated(this);
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
        if (_canDropPOW && _vulkanPOWsGO != null)
        {
            /*switch (_type)
            {
                case VulkanPOWType.Orange:
                    Instantiate(_vulkanPOWsGO.vulkanPOWsGO[0], transform.position, Quaternion.identity);
                    break;
                case VulkanPOWType.Blue:
                    Instantiate(_vulkanPOWsGO.vulkanPOWsGO[1], transform.position, Quaternion.identity);
                    break;
                default:
                    break;
            }*/
        }  
    }
    
}
