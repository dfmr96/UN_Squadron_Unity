using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour, IDamagable, IFire
{
    protected bool _customAnim;
    protected SpriteRenderer _spriteRenderer;
    protected Sprite[] _sprites;
    
    
    protected float _health;
    protected int _moveSpeed = 3;
    
    protected bool _canDropPOW;
    protected VulkanPOWType _type;
    [SerializeField] VulkanPOWs _vulkanPOWsGO;
    [SerializeField] protected PlayerGO _player;

    protected float _fireRateCounter;
    protected float _fireRate;
    [SerializeField] protected GameObject _explosionPrefab;
    [SerializeField] protected GameObject _bulletPrefab;

    
    protected float _collisionDamage;
   
    
    
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
    
    
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(_collisionDamage);
            DestroyEnemy();
        }
    }
    
    protected void DestroyEnemy()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        AudioManager.instance.enemyDestroyedAudio.Play();
        //EventBus.instance.EnemyDestroyed(this);
        Destroy(gameObject);
        Debug.Log("Enemy Destroyed");
    }
    
    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            DropItem();
            DestroyEnemy();
        }
    }
    
    protected virtual void Fire(Vector3 dir)
    {
        var angle = Vector3.Angle(transform.right, dir);
        GameObject bullet = null;

        bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().SetDirection(dir);
        _fireRateCounter = 0;
    }

    protected Vector3 AimDirection()
    {
        return (_player.Player.transform.position - transform.position).normalized;
    }

    private void DropItem()
    {
        if (_canDropPOW)
        {
            switch (_type)
            {
                case VulkanPOWType.Orange:
                    Instantiate(_vulkanPOWsGO.vulkanPOWsGO[0], transform.position, Quaternion.identity);
                    break;
                case VulkanPOWType.Blue:
                    Instantiate(_vulkanPOWsGO.vulkanPOWsGO[1], transform.position, Quaternion.identity);
                    break;
                default:
                    break;
            }
        }  
    }
    #region TankLogics
    private void MoveTank(Vector3 dir)
    {
        transform.Translate(new Vector3(dir.x, 0, 0) * _moveSpeed * Time.deltaTime);
    }

    private void ChangeTankSprite(Vector3 dir)
    {
        float angle = (int)Vector3.Angle(dir, -transform.right);
        //Debug.Log(angle);
        if (angle >= 0 && angle < 20)
        {
            _spriteRenderer.sprite = _sprites[0];
        }
        else if (angle >= 20 && angle < 40)
        {
            _spriteRenderer.sprite = _sprites[1];
        }
        else if (angle >= 40 && angle < 60)
        {
            _spriteRenderer.sprite = _sprites[2];
        }
        else if (angle >= 60 && angle < 80)
        {
            _spriteRenderer.sprite = _sprites[3];
        }
        else if (angle >= 80 && angle < 100)
        {
            _spriteRenderer.sprite = _sprites[4];
        }
        else if (angle >= 100 && angle < 120)
        {
            _spriteRenderer.sprite = _sprites[5];
        }
        else if (angle >= 120 && angle < 140)
        {
            _spriteRenderer.sprite = _sprites[6];
        }
        else if (angle >= 140 && angle < 160)
        {
            _spriteRenderer.sprite = _sprites[7];
        }
        else if (angle >= 160 && angle < 180)
        {
            _spriteRenderer.sprite = _sprites[8];
        }
    }
    #endregion TankLogics

    

}
