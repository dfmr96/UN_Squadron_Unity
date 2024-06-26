using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class Tank : Enemy
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private bool canDrop;

   // [SerializeField] private VulkanPOWType dropType;

    [SerializeField] private EnemySprites tankSprite;
    
    private void Awake()
    {
        enemyDataParent = enemyData;
        _health = enemyData.health;
        _moveSpeed = enemyData.moveSpeed;
        
        _collisionDamage = enemyData.collisionDamage;
        _explosionPrefab = enemyData.explosionPrefab;
        
        

        //_type = dropType;

        
        if (Random.value >= 0.5)
        {
            _canDropPOW = true;
        }
        else
        {
            _canDropPOW = false;
        }
        
        _sprites = tankSprite.enemySprites;
        _customAnim = enemyData.customAnim;
        
        _fireRate = enemyData.fireRate;
        _bulletPrefab = enemyData.bulletPrefab;

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
        ChangeTankSprite(AimDirection());
        if (!_customAnim)
        {
            MoveTank(AimDirection());
        }
        if (_fireRateCounter > 1 / _fireRate)
        {
            Fire();
            Debug.Log("Fire");
        }

        
    }

    private void MoveTank(Vector3 dir)
    {
        transform.Translate(new Vector3(dir.x, 0, 0) * _moveSpeed * Time.deltaTime);
    }

    private void ChangeTankSprite(Vector3 dir)
    {
        const float angleStep = 20f;
        float angle = Vector3.Angle(dir, -transform.right);
        int spriteIndex = Mathf.Clamp((int)(angle / angleStep), 0, _sprites.Length - 1);
    
        _spriteRenderer.sprite = _sprites[spriteIndex];
    }
    
    private void OnEnable()
    {
        Awake();
    }
} 