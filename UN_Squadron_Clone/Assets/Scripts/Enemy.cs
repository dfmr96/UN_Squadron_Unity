using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyType
{
    helo,
    turret,
    bigTurret,
    tank,
    boss
}

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private int _health;
    [SerializeField] int _moveSpeed = 3;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _fireRateCounter;
    [SerializeField] private bool _canDropPOW;
    [SerializeField] private VulkanPOWType _type;
    [SerializeField] private GameObject[] _vulkanPOWs;
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Vector3 _aimDir = Vector3.zero;
    [SerializeField] bool CustomAnim = false;
    [SerializeField] float collisionDamage;

    //Big Turret Settings
    private float _angleToShoot = 0;
    private Vector3 _raycastDir = Vector3.zero;
    [SerializeField] int _bulletToShoot = 0;
    [SerializeField] float _timeBetweenBullets = 0;

    public int scorePerKill = 100;
    public int moneyPerKill = 300;

    private void Start()
    {
        if (CustomAnim) GetComponent<Animator>().SetBool("CustomAnim", CustomAnim);
        _player = FindObjectOfType<PlayerController>().gameObject;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        DeactiveAllComponents();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            if (collision.gameObject.GetComponent<PlayerController>().isInvulnerable) return;
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(collisionDamage);
            DestroyEnemy();
        }
    }

    private void Update()
    {
        _fireRateCounter += Time.deltaTime;

        _aimDir = (_player.transform.position - transform.position).normalized;

        //Enemies Movement and Animations

        switch (_enemyType)
        {
            case EnemyType.turret:
                ChangeTurretSprite(_aimDir);
                break;
            case EnemyType.bigTurret:
                ChangeTurretSprite(_aimDir);
                break;
            case EnemyType.tank:
                MoveTank(_aimDir);
                ChangeTankSprite(_aimDir);
                break;
            case EnemyType.helo:
                if (CustomAnim) break;
                MoveHelo();
                break;
            default:
                break;
        }
        //if (_enemyType == EnemyType.turret || _enemyType == EnemyType.bigTurret) ChangeTurretSprite(_aimDir);
        //if (_enemyType == EnemyType.tank)
        //{
        //    MoveTank(_aimDir);
        //    ChangeTankSprite(_aimDir);
        //}

        //Enemies Attack
        if (_fireRateCounter > 1 / _fireRate)
        {
            switch (_enemyType)
            {
                case EnemyType.bigTurret:
                    InvokeRaycast();
                    break;
                case EnemyType.boss:
                    break;
                default:
                    Fire(_aimDir);
                    break;
            }
            //if (_enemyType != EnemyType.bigTurret) FireBullet(_aimDir);
            //if (_enemyType == EnemyType.bigTurret)
            //{
            //    InvokeRaycast();
            //}
        }
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            if (_canDropPOW)
            {
                switch (_type)
                {
                    case VulkanPOWType.Orange:
                        Instantiate(_vulkanPOWs[0], transform.position, Quaternion.identity);
                        break;
                    case VulkanPOWType.Blue:
                        Instantiate(_vulkanPOWs[1], transform.position, Quaternion.identity);
                        break;
                    default:
                        break;
                }
            }
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        AudioManager.instance.enemyDestroyedAudio.Play();
        EventBus.instance.EnemyDestroyed(this);
        Destroy(gameObject);
        Debug.Log("Enemy Destroyed");
    }

    public void Fire(Vector3 dir)
    {
        var angle = Vector3.Angle(transform.right, dir);
        GameObject bullet = null;

        if (_enemyType != EnemyType.bigTurret)
        {
            bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetDirection(dir);
            _fireRateCounter = 0;
        }
        else
        {
            //bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.Euler(0, 0, angleToShoot));
            //bullet.GetComponent<EnemyBullet>().SetDirection(Vector3.right);
            StartCoroutine(BigTurretBurst(bullet));
        }

    }

    public void MoveHelo()
    {
        transform.Translate(transform.right * _moveSpeed * Time.deltaTime);
    }
    public void DeactiveAllComponents()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        Behaviour[] comps = gameObject.GetComponents<Behaviour>();
        foreach (Behaviour comp in comps)
        {
            //Debug.Log(comp);
            comp.enabled = false;
        }
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ChangeTurretSprite(Vector3 dir)
    {
        float angle = (int)Vector3.Angle(dir, -transform.right);
        _spriteRenderer.flipX = false;
        //Debug.Log(angle);
        if (angle >= 0 && angle < 20)
        {
            _spriteRenderer.sprite = _sprites[0];
            _angleToShoot = 180;
        }
        else if (angle >= 20 && angle < 40)
        {
            _spriteRenderer.sprite = _sprites[1];
            _angleToShoot = 150;
        }
        else if (angle >= 40 && angle < 60)
        {
            _spriteRenderer.sprite = _sprites[2];
            _angleToShoot = 135;
        }
        else if (angle >= 60 && angle < 80)
        {
            _spriteRenderer.sprite = _sprites[3];
            _angleToShoot = 120;
        }
        else if (angle >= 80 && angle < 100)
        {
            _spriteRenderer.sprite = _sprites[4];
            _angleToShoot = 90;

        }
        else if (angle >= 100 && angle < 120)
        {
            _spriteRenderer.sprite = _sprites[3];
            _spriteRenderer.flipX = true;
            _angleToShoot = 60;
        }
        else if (angle >= 120 && angle < 140)
        {
            _spriteRenderer.sprite = _sprites[2];
            _spriteRenderer.flipX = true;
            _angleToShoot = 45;
        }
        else if (angle >= 140 && angle < 160)
        {
            _spriteRenderer.sprite = _sprites[1];
            _spriteRenderer.flipX = true;
            _angleToShoot = 35;
        }
        else if (angle >= 160 && angle < 180)
        {
            _spriteRenderer.sprite = _sprites[0];
            _spriteRenderer.flipX = true;
            _angleToShoot = 0;
        }

        //int i = 20;
        ////_spriteRenderer.sprite = _sprites[(int)Math.Round(Convert.ToDecimal(angle / i) * 10)];
        //Debug.Log("Sprite #" + (int)Math.Round(Convert.ToDecimal((angle / i)/10) * 10));
    }
    #region TankLogics
    public void MoveTank(Vector3 dir)
    {
        transform.Translate(new Vector3(dir.x, 0, 0) * _moveSpeed * Time.deltaTime);
    }

    public void ChangeTankSprite(Vector3 dir)
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
    #endregion TankLogics
    #region Big Turret Logic
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
            Fire(Vector3.zero);
            Debug.Log("Torreta dispara a Jugador");
        }
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
    #endregion Big Turret Logic
    private void OnDrawGizmos()
    {
        if (_enemyType == EnemyType.bigTurret)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, _raycastDir * 20);
        }
    }
}
