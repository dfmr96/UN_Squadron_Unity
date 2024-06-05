using System;
using UnityEngine;

public class Helo : Enemy
{
    [SerializeField] private EnemyData _enemyData;

    [SerializeField] private bool _canDrop;
    //[SerializeField] private VulkanPOWType _dropType; TODO
    [SerializeField] private Sprite[] _heloSprite;
    private void Awake()
    {
        _health = _enemyData._health;
        _customAnim = _enemyData._customAnim;
        _fireRate = _enemyData._fireRate;
        _collisionDamage = _enemyData._collisionDamage;
        _health = _enemyData._health;
        _canDropPOW = _canDrop;
        //_type = _dropType; TODO
        _sprites = _heloSprite;
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
            Fire(AimDirection());
        }
        
    }

    private void MoveHelo()
    {
        transform.Translate(transform.right * _moveSpeed * Time.deltaTime);
    }
}