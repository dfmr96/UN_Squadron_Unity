using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class Helo : Enemy
{
    [SerializeField] private EnemyData _enemyData;
    
    //[SerializeField] private VulkanPOWType _dropType;
    [SerializeField] private EnemySprites _heloSprite;
    //[SerializeField] public GameObject player;
    private Vector3 aimdir;
    private void Awake()
    {
        _health = _enemyData.health;
        _moveSpeed = _enemyData.moveSpeed;
        
        _collisionDamage = _enemyData.collisionDamage;
        _explosionPrefab = _enemyData.explosionPrefab;
        
        
      //  _type = _dropType;
        
        if ( Random.value >= 0.5)
        {
            _canDropPOW = true;
        }
        else
        {
            _canDropPOW = false;
        }
        
        
        _sprites = _heloSprite.enemySprites;
        _customAnim = _enemyData.customAnim;
        
        
        
        
        _fireRate = _enemyData.fireRate;
        _bulletPrefab = _enemyData.bulletPrefab;
        
        
    }
    
    private void Start()
    {
        if (_customAnim) GetComponent<Animator>().SetBool("CustomAnim", _customAnim);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        DeactiveAllComponents();
    }

    private void Update()
    {
        _fireRateCounter += Time.deltaTime;
 
        
        if (!_customAnim)
        {
            MoveHelo();
        }
        if (_fireRateCounter > 1 / _fireRate)
        {
            Fire();
        }
    }

    private void MoveHelo()
    {
        transform.Translate(transform.right * _moveSpeed * Time.deltaTime);
    }
}