using System.Collections;
using DefaultNamespace;
using Pickupables;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

public enum PlayerState
{
    healthy,
    danger,
    critical,
    destroyed
}

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [Space(10)]
    public float health;
    public float maxHealth;
    [SerializeField] PlayerState _state;
    [SerializeField] float _speed = 15;
    [SerializeField] float invulnerabilityTime;
    public bool isInvulnerable;
    
    [SerializeField] int _recoveryTime;
    [FormerlySerializedAs("_camera")]
    [Space(20)]
    [Header("GameObjects")]
    [Space(10)]
    [SerializeField] SideScrollController sideScroll;
    [SerializeField] GameObject _damagedFlames;

    [SerializeField] Sprite[] _aircraftSprites;
    private SpriteRenderer _aircraftRenderer;
    [SerializeField] Animator _anim;

    //Privados
    private float _horizontal;
    private float _vertical;
    private BoxCollider2D _cameraCol;
    private BoxCollider2D _playerCol;

    [field: SerializeField] public Vulkan FrontVulkan { get; private set; }

    private void OnEnable()
    {
        EventBus.instance.OnBossDestroyed += OnBossDestroyed;
    }
    private void OnBossDestroyed()
    {
        AudioManager.instance.playerDamaged.Stop();
        isInvulnerable = true;
        enabled = false;
    }
    private void OnDisable()
    {
        EventBus.instance.OnBossDestroyed -= OnBossDestroyed;
    }
    private void Start()
    {
        FrontVulkan.InitVulkan();
        GetReferences();
        isInvulnerable = false;
        health = maxHealth;
        EventBus.instance.PlayerSpawned(this);
    }
    private void GetReferences()
    {
        _cameraCol = sideScroll.Bounds;
        transform.parent = sideScroll.transform;
        _playerCol = GetComponent<BoxCollider2D>();
        _aircraftRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        FrontVulkan.Update();
        Movement();
        if (Input.GetKey(KeyCode.Space)) FrontVulkan.TryFire();
    }
    private void Movement()
    {
        if (_state == PlayerState.destroyed) return;
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 dir = new Vector3(_horizontal, _vertical).normalized;
        CheckBounds();
        transform.Translate(dir * (_speed * Time.deltaTime));

        _anim.SetInteger("Vertical", (int)_vertical);
    }
    private void CheckBounds()
    {
        //Chequeo de colisiones, si el jugador intenta atravesar la pantalla no podra
        if (_playerCol.bounds.min.x < _cameraCol.bounds.min.x && _horizontal == -1)
        {
            _horizontal = 0;
        }

        if (_playerCol.bounds.max.x > _cameraCol.bounds.max.x && _horizontal == 1)
        {
            _horizontal = 0;
        }

        if (_playerCol.bounds.min.y < _cameraCol.bounds.min.y && _vertical == -1)
        {
            _vertical = 0;
        }

        if (_playerCol.bounds.max.y > _cameraCol.bounds.max.y && _vertical == 1)
        {
            _vertical = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IPickupable pickupable))
        {
            pickupable.PickUp(this);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("Impacto contra bala");
            if (collision.gameObject.CompareTag("Obstacles"))
            {
                TakeDamage(2);
            }
        }
    }
    private IEnumerator GetRecovery()
    {
        AudioManager.instance.playerRecovery.Play();
        yield return new WaitForSeconds(_recoveryTime);
        EventBus.instance.PlayerRecovered();
        _state = PlayerState.healthy;
        _damagedFlames.SetActive(false);
        _anim.SetInteger("PlayerState", (int)_state);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable) return;
        StartCoroutine(Invulnerability());
        switch (_state)
        {
            case PlayerState.healthy:
                health -= damage;
                _state = PlayerState.danger;
                _anim.SetInteger("PlayerState", (int)_state);
                _damagedFlames.SetActive(true);
                AudioManager.instance.playerDamaged.Play();
                EventBus.instance.PlayerDamaged(damage);
                if (health <= 0)
                {
                    AudioManager.instance.playerUnableToRecover.Play();
                    return;
                }
                StartCoroutine(GetRecovery());
                break;
            case PlayerState.danger:
                DestroyPlayer();
                break;
            default:
                break;
        }
    }

    private void DestroyPlayer()
    {
        _state = PlayerState.destroyed;
        StopAllCoroutines();
        StartCoroutine(GameOver());
        _anim.SetInteger("PlayerState", (int)_state);
        _damagedFlames.SetActive(false);
        AudioManager.instance.playerRecovery.Stop();
        AudioManager.instance.bgmAudio.Stop();
        AudioManager.instance.playerDestroyed.Play();
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.GameOver();
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }
}
