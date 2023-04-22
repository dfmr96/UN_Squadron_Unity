using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Audio;

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
    

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>().gameObject;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        DeactiveAllComponents();
    }

    private void Update()
    {
        _fireRateCounter += Time.deltaTime;

        Vector3 aimDir = (_player.transform.position - transform.position).normalized;
        if (_enemyType == EnemyType.turret) ChangeTurretSprite(aimDir);
        if (_enemyType == EnemyType.tank)
        {
            MoveTank(aimDir);
            ChangeTankSprite(aimDir);
        }
        if (_fireRateCounter > 1 / _fireRate)
        {
            FireBullet(aimDir);
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
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void FireBullet(Vector3 dir)
    {
        var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().SetDirection(dir);
        _fireRateCounter = 0;
    }

    public void DeactiveAllComponents()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        Behaviour[] comps = gameObject.GetComponents<Behaviour>();
        foreach (Behaviour comp in comps)
        {
            Debug.Log(comp);
            comp.enabled = false;
        }
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ChangeTurretSprite(Vector3 dir)
    {
        float angle = (int)Vector3.Angle(dir, -transform.right);
        _spriteRenderer.flipX = false;
        Debug.Log(angle);
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
            _spriteRenderer.sprite = _sprites[3];
            _spriteRenderer.flipX = true;
        }
        else if (angle >= 120 && angle < 140)
        {
            _spriteRenderer.sprite = _sprites[2];
            _spriteRenderer.flipX = true;
        }
        else if (angle >= 140 && angle < 160)
        {
            _spriteRenderer.sprite = _sprites[1];
            _spriteRenderer.flipX = true;
        }
        else if (angle >= 160 && angle < 180)
        {
            _spriteRenderer.sprite = _sprites[0];
            _spriteRenderer.flipX = true;
        }

        //int i = 20;
        ////_spriteRenderer.sprite = _sprites[(int)Math.Round(Convert.ToDecimal(angle / i) * 10)];
        //Debug.Log("Sprite #" + (int)Math.Round(Convert.ToDecimal((angle / i)/10) * 10));
    }

    public void MoveTank(Vector3 dir)
    {
        transform.Translate(new Vector3(dir.x,0, 0) * 4 * Time.deltaTime);
    }

    public void ChangeTankSprite(Vector3 dir)
    {
        float angle = (int)Vector3.Angle(dir, -transform.right);
        Debug.Log(angle);
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
