using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class DefaultTurret: Enemy
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private bool _canDrop;

    private float _angleToShoot;
    [SerializeField] private EnemySprites _defaultTurretSprites;

    [SerializeField] private GameObject player;
    public void Awake()
    {

        enemyDataParent = enemyData;
        _health = enemyData.health;
        _moveSpeed = enemyData.moveSpeed;
        
        _collisionDamage = enemyData.collisionDamage;
        _explosionPrefab = enemyData.explosionPrefab;

        
        if (Random.value >= 0.5)
        {
            _canDropPOW = true;
        }
        else
        {
            _canDropPOW = false;
        }
        
        _sprites = enemyData.EnemySprites.enemySprites;
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
        ChangeTurretSprite(AimDirection());
        if (_fireRateCounter > 1 / _fireRate)
        {
           Fire();
        }
    }
    
    private void ChangeTurretSprite(Vector3 dir)
    {
        float angle = Vector3.Angle(dir, -transform.right);
        bool flipX = false;
        Sprite selectedSprite = null;
        int angleToShoot = 0;

        var anglesMap = new (float, float, Sprite, int)[]
        {
            (0, 20, _sprites[0], 180),
            (20, 40, _sprites[1], 150),
            (40, 60, _sprites[2], 135),
            (60, 80, _sprites[3], 120),
            (80, 100, _sprites[4], 90),
            (100, 120, _sprites[3], 60),
            (120, 140, _sprites[2], 45),
            (140, 160, _sprites[1], 35),
            (160, 180, _sprites[0], 0),
        };

        foreach (var (minAngle, maxAngle, sprite, shootAngle) in anglesMap)
        {
            if (angle >= minAngle && angle < maxAngle)
            {
                selectedSprite = sprite;
                angleToShoot = shootAngle;
                if (minAngle >= 100)
                {
                    flipX = true;
                }
                break;
            }
        }

        _spriteRenderer.sprite = selectedSprite;
        _spriteRenderer.flipX = flipX;
        _angleToShoot = angleToShoot;
    }
    private void OnEnable()
    {
        Awake();
    }
}
