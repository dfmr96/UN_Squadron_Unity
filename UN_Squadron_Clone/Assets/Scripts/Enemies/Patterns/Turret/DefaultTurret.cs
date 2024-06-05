using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class DefaultTurret: Turret
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private bool _canDrop;
    [SerializeField] private VulkanPOWType _dropType;
    [SerializeField] private EnemySprites _defaultTurretSprites;
    [SerializeField] private GameObject player;
    public void Awake()
    {
        _health = enemyData.health;
        _moveSpeed = enemyData.moveSpeed;
        
        _collisionDamage = enemyData.collisionDamage;
        _explosionPrefab = enemyData.explosionPrefab;
        
        
        _type = _dropType;
        
        if (Random.value >= 0.5)
        {
            _canDropPOW = true;
        }
        else
        {
            _canDropPOW = false;
        }
        
        _sprites = _defaultTurretSprites.enemySprites;
        _customAnim = enemyData.customAnim;
        
        _fireRate = enemyData.fireRate;
        _bulletPrefab = enemyData.bulletPrefab;

        _player = player;
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
        ChangeTurretSprite(AimDirection());
        if (_fireRateCounter > 1 / _fireRate)
        {
           Fire();
        }
    }
}
