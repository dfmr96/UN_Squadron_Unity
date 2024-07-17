﻿using System.Collections;
using Enemies.Core;
using ScriptableObjects.Enemies.EnemyData;
using ScriptableObjects.Enemies.EnemySprites;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies.EnemyBehaviour.Turret
{
    public class BigTurret: Enemy
    {
        [SerializeField] private EnemyData enemyData;
        [SerializeField] private bool canDrop;
        private float _angleToShoot;
        [SerializeField] private int bulletToShoot;
        [SerializeField] private float timeBetweenBullets;
        [SerializeField] private EnemySprites bigTurretSprite;
        private Vector3 _raycastDir = Vector3.zero;
        private void InitData()
        {
            enemyDataParent = enemyData;
            _health = enemyData.health;
            _customAnim = enemyData.customAnim;
            _fireRate = enemyData.fireRate;
            _collisionDamage = enemyData.collisionDamage;
            _health = enemyData.health;
            _canDropPOW = canDrop;
            _explosionPrefab = enemyData.explosionPrefab;
            _sprites = bigTurretSprite.enemySprites;
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
                InvokeRaycast();
            }
        }
        private void InvokeRaycast()
        {
            _raycastDir = new Vector3(Mathf.Cos(Mathf.Deg2Rad * _angleToShoot), Mathf.Sin(Mathf.Deg2Rad * _angleToShoot), 0);
            Debug.Log(_raycastDir);
            int layerMask = LayerMask.GetMask("Player");
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

        private IEnumerator BigTurretBurst(GameObject bullet)
        {
            _fireRateCounter = - timeBetweenBullets * bulletToShoot;
            //float fixedAngle = angleToShoot;
            for (int i = 0; i <= bulletToShoot; i++)
            {
                bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.Euler(0, 0, _angleToShoot));
                bullet.GetComponent<EnemyBullet>().SetDirection(Vector3.right);
                yield return new WaitForSeconds(timeBetweenBullets);
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
            InitData();
        }
    }
}
