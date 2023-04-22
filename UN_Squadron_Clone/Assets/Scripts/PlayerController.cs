using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum PlayerState
{
    alive,
    danger,
    critical,
    exploted
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
        Movement();

        FireVulkan();
    }

    private void FireVulkan()
    {
        _vulkanCounter += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && _vulkanCounter > 1 / _vulkanFireRate)
        {
            Instantiate(_currentVulkanBullet, _vulkanCannon.transform.position, Quaternion.identity);
            _vulkanCounter = 0;
        }
    }

    private void Movement()
    {
        if (_state == PlayerState.exploted) return;
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        
        CheckCameraBounds();

        Vector3 _dir = new Vector3(_horizontal, _vertical).normalized;
        transform.Translate(_dir * _speed * Time.deltaTime);

        ChangeSprites();
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
            _currentVulkan += collision.GetComponent<VulkanPOW>().IncreaseVulkanPOWPoints();
            CheckVulkanPoints();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.GetComponent<EnemyBullet>() != null || collision.gameObject.GetComponent<Enemy>() != null)
        {
            Debug.Log("Impacto contra bala");
            Destroy(collision.gameObject);
            switch (_state)
            {
                case PlayerState.alive:
                    _state = PlayerState.danger;
                    _anim.SetInteger("PlayerState", (int)_state);
                    _damagedFlames.SetActive(true);
                    StartCoroutine(GetRecovery());
                    break;
                case PlayerState.danger:
                    _state = PlayerState.exploted;
                    StopAllCoroutines();
                    _anim.SetInteger("PlayerState", (int)_state);
                    _damagedFlames.SetActive(false);
                    break;
                case PlayerState.critical:
                    break;
                case PlayerState.exploted:
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

    public void ChangeSprites()
    {
        _anim.SetInteger("Vertical", (int)_vertical);
        switch (_vertical)
        {
            case -1:
                _aircraftRenderer.sprite = _aircraftSprites[0];
                break;
            case 0:
                _aircraftRenderer.sprite = _aircraftSprites[1];
                break;
            case 1:
                _aircraftRenderer.sprite = _aircraftSprites[2];
                break;
            default:
                break;
        }
    }

    public IEnumerator GetRecovery()
    {
        yield return new WaitForSeconds(_recoveryTime);
        _state = PlayerState.alive;
        _damagedFlames.SetActive(false);
        _anim.SetInteger("PlayerState", (int)_state);
    }
}
