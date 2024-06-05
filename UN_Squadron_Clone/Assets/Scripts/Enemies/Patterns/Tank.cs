using System;
using UnityEngine;
public class Tank : Enemy
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private bool canDrop;
    //[SerializeField] private VulkanPOWType dropType; //TODO
    [SerializeField] private Sprite[] tankSprite;
    
    private void Awake()
    {
        _health = enemyData._health;
        _customAnim = enemyData._customAnim;
        _fireRate = enemyData._fireRate;
        _collisionDamage = enemyData._collisionDamage;
        _health = enemyData._health;
        _canDropPOW = canDrop;
        //_type = dropType; TODO
        _sprites = tankSprite;
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
            Fire(AimDirection());
        }
    }

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
} 