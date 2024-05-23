using System;
using UnityEngine;

public class Helo : Enemy
{
    [SerializeField] private EnemyData _enemyData;

    [SerializeField] private bool _canDrop;
    [SerializeField] private VulkanPOWType _dropType;
    [SerializeField] private EnemySprites _heloSprite;
    [SerializeField] private GameObject player;
    private Vector3 aimdir;
    private void Awake()
    {
        _health = _enemyData._health;
        _customAnim = _enemyData._customAnim;
        _fireRate = _enemyData._fireRate;
        _collisionDamage = _enemyData._collisionDamage;
        _canDropPOW = _canDrop;
        _type = _dropType;
        _sprites = _heloSprite.enemySprites;
        _player = player;
        _explosionPrefab = _enemyData.explosionPrefab;
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