using System.Collections;
using UnityEngine;

public enum PlayerState
{
    healthy,
    danger,
    critical,
    destroyed
}
public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [Space(10)]
    [SerializeField] PlayerState _state;
    [SerializeField] float _speed = 15;
    [SerializeField] float _vulkanFireRate;
    [SerializeField] float _vulkanCounter;
    [SerializeField] int _currentVulkan;
    [SerializeField] GameObject _currentVulkanBullet;
    [SerializeField] int _pointsToNextVulkan;
    [SerializeField] int _nextVulkanPoints;
    [SerializeField] int _currentVulkanLevel;
    [SerializeField] int[] _vulkanLevels = { 0, 2, 9, 18 };
    [SerializeField] int _recoveryTime;
    [Space(20)]
    [Header("GameObjects")]
    [Space(10)]
    [SerializeField] GameObject _camera;
    [SerializeField] GameObject[] _vulkanBullets;
    [SerializeField] GameObject _vulkanCannon;
    [SerializeField] GameObject _damagedFlames;

    [SerializeField] Sprite[] _aircraftSprites;
    SpriteRenderer _aircraftRenderer;
    [SerializeField] Animator _anim;

    //Privados
    float _horizontal;
    float _vertical;
    private BoxCollider2D _cameraCol;
    private BoxCollider2D _playerCol;
    private void Start()
    {
        _camera = FindObjectOfType<CameraController>().gameObject;
        _cameraCol = _camera.GetComponentInChildren<BoxCollider2D>();
        _playerCol = GetComponent<BoxCollider2D>();
        transform.parent = _camera.transform;
        _aircraftRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        _nextVulkanPoints = _vulkanLevels[_currentVulkanLevel + 1] - _currentVulkan;
        SetVulkanBullet();
    }


    private void Update()
    {
        //Debug.Log(GetComponent<Rigidbody2D>().velocity.magnitude);
        Movement();

        FireVulkan();
    }

    private void FireVulkan()
    {
        _vulkanCounter += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && _vulkanCounter > 1 / _vulkanFireRate && _state != PlayerState.destroyed)
        {
            Instantiate(_currentVulkanBullet, _vulkanCannon.transform.position, Quaternion.identity);
            AudioManager.instance.vulkanAudio.Play();
            _vulkanCounter = 0;
        }
    }

    private void Movement()
    {
        if (_state == PlayerState.destroyed) return;
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        CheckCameraBounds();

        Vector3 _dir = new Vector3(_horizontal, _vertical).normalized;
        transform.Translate(_dir * _speed * Time.deltaTime);

        _anim.SetInteger("Vertical", (int)_vertical);
    }

    private void CheckCameraBounds()
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
        if (collision.gameObject.GetComponent<VulkanPOW>() != null)
        {
            AudioManager.instance.vulkanPOW.Play();
            _currentVulkan += collision.GetComponent<VulkanPOW>().IncreaseVulkanPOWPoints();
            CheckVulkanPoints();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.GetComponent<EnemyBullet>() != null || collision.gameObject.GetComponent<Enemy>() != null || collision.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("Impacto contra bala");
            if (!collision.gameObject.CompareTag("Obstacles")) Destroy(collision.gameObject);
            switch (_state)
            {
                case PlayerState.healthy:
                    _state = PlayerState.danger;
                    _anim.SetInteger("PlayerState", (int)_state);
                    _damagedFlames.SetActive(true);
                    AudioManager.instance.playerDamaged.Play();

                    StartCoroutine(GetRecovery());
                    break;
                case PlayerState.danger:
                    _state = PlayerState.destroyed;
                    StopAllCoroutines();
                    _anim.SetInteger("PlayerState", (int)_state);
                    _damagedFlames.SetActive(false);
                    AudioManager.instance.playerRecovery.Stop();
                    AudioManager.instance.bgmAudio.Stop();
                    AudioManager.instance.playerDestroyed.Play();
                    break;
                default:
                    break;
            }
        }
    }

    private void CheckVulkanPoints()
    {
        int pointsExceed = 0;
        if (_currentVulkan > _nextVulkanPoints)
        {
            pointsExceed = _currentVulkan - _nextVulkanPoints;
            _currentVulkan = _nextVulkanPoints;
        }
        if (_currentVulkan == _nextVulkanPoints)
        {
            _currentVulkanLevel++;
            SetVulkanBullet();
        }
        _currentVulkan += pointsExceed;
        _nextVulkanPoints = _vulkanLevels[_currentVulkanLevel + 1];
        _pointsToNextVulkan = _nextVulkanPoints - _currentVulkan;
    }

    public void SetVulkanBullet()
    {
        _currentVulkanBullet = _vulkanBullets[_currentVulkanLevel];
    }

    public IEnumerator GetRecovery()
    {
        AudioManager.instance.playerRecovery.Play();
        yield return new WaitForSeconds(_recoveryTime);
        _state = PlayerState.healthy;
        _damagedFlames.SetActive(false);
        _anim.SetInteger("PlayerState", (int)_state);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
