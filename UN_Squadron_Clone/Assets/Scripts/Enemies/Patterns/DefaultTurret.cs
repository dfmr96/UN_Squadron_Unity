using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Enemies.Patterns;
public class DefaultTurret: Turret
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private bool _canDrop;
    //[SerializeField] private VulkanPOWType _dropType; TODO
    [SerializeField] private EnemySprites _defaultTurretSprites;
    public void Awake()
    {
        _customAnim = _enemyData._customAnim;
        _fireRate = _enemyData._fireRate;
        _collisionDamage = _enemyData._collisionDamage;
        _health = _enemyData._health;
        _canDropPOW = _canDrop;
        //_type = _dropType; TODO
        _sprites = _defaultTurretSprites;
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
           Fire(AimDirection());
        }
    }
}
