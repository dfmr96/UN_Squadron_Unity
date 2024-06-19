using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class BigTurret: Turret
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private bool _canDrop;

   // [SerializeField] private VulkanPOWType _dropType;

    [SerializeField] private int _bulletToShoot;
    [SerializeField] private float _timeBetweenBullets;
    [SerializeField] private EnemySprites _bigTurretSprite;
    private Vector3 _raycastDir = Vector3.zero;
    public void Awake()
    {
        _health = _enemyData.health;
        _customAnim = _enemyData.customAnim;
        _fireRate = _enemyData.fireRate;
        _collisionDamage = _enemyData.collisionDamage;
        _health = _enemyData.health;
        _canDropPOW = _canDrop;

       // _type = _dropType;

        _sprites = _bigTurretSprite.enemySprites;
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
        ChangeTurretSprite(AimDirection());
        if (_fireRateCounter > 1 / _fireRate)
        {
            InvokeRaycast();
        }
    }

    public void InvokeRaycast()
    {
        _raycastDir = new Vector3(Mathf.Cos(Mathf.Deg2Rad * _angleToShoot), Mathf.Sin(Mathf.Deg2Rad * _angleToShoot), 0);
        Debug.Log(_raycastDir);
        //Debug.Log(angleToShoot);
        int layerMask = LayerMask.GetMask("Player");
        //Debug.Log(layerMask);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _raycastDir, 20, layerMask);
        if (hit.collider != null)
        {
            Fire();
            Debug.Log("Torreta dispara a Jugador");
        }
    }

    protected override void Fire()
    {
        StartCoroutine(BigTurretBurst(_bulletPrefab));
    }
    
    public IEnumerator BigTurretBurst(GameObject bullet)
    {
        _fireRateCounter = - _timeBetweenBullets * _bulletToShoot;
        //float fixedAngle = angleToShoot;
        for (int i = 0; i <= _bulletToShoot; i++)
        {
            bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.Euler(0, 0, _angleToShoot));
            bullet.GetComponent<EnemyBullet>().SetDirection(Vector3.right);
            yield return new WaitForSeconds(_timeBetweenBullets);
        }
    }
}
