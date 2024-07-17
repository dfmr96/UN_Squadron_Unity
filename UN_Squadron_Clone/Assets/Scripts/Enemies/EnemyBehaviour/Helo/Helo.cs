using Enemies.Core;
using ScriptableObjects.Enemies.EnemyData;
using ScriptableObjects.Enemies.EnemySprites;
using UnityEngine;
using UnityEngine.Serialization;


namespace Enemies.EnemyBehaviour.Helo
{
    public class Helo : Enemy
    {
        [SerializeField] private EnemyData enemyData;
        [SerializeField] private EnemySprites heloSprite;
        private Vector3 aimdir;
        private void InitData()
        {
            _health = enemyData.health;
            _moveSpeed = enemyData.moveSpeed;
            _collisionDamage = enemyData.collisionDamage;
            _explosionPrefab = enemyData.explosionPrefab;
            enemyDataParent = enemyData;
            //_sprites = heloSprite.enemySprites;
            _customAnim = enemyData.customAnim;
            _fireRate = enemyData.fireRate;
            _bulletPrefab = enemyData.bulletPrefab;
        }

        private void Start()
        {
            InitData();
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
            transform.Translate(transform.right * (_moveSpeed * Time.deltaTime));
        }

        private void OnEnable()
        {
            InitData();
        }
    }
}