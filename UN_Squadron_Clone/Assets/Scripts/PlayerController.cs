using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [Space(10)]
    [SerializeField] float _speed = 15;
    [SerializeField] float _vulkanFireRate;
    [SerializeField] float _vulkanCounter;
    [SerializeField] int _currentVulkan;
    [SerializeField] int _pointsToNextVulkan;
    [SerializeField] int _nextVulkanPoints;
    [SerializeField] int _currentVulkanLevel;
    [SerializeField] int[] _vulkanLevels = { 0, 2, 9, 18 };
    [Space(20)]
    [Header("GameObjects")]
    [Space(10)]
    [SerializeField] GameObject _camera;
    [SerializeField] GameObject _vulkanBullet;
    [SerializeField] GameObject _vulkanCannon;

    //Privados
    float _horizontal;
    float _vertical;
    private BoxCollider2D _cameraCol;
    private BoxCollider2D _playerCol;
    private void Start()
    {
        //Seteo de variables
        _camera = FindObjectOfType<CameraController>().gameObject;
        _cameraCol = _camera.GetComponentInChildren<BoxCollider2D>();
        _playerCol = GetComponent<BoxCollider2D>();
        //El jugador se vuelve hijo de la camara para que la pueda seguir
        transform.parent = _camera.transform;

        _nextVulkanPoints = _vulkanLevels[_currentVulkanLevel + 1] - _currentVulkan;
    }


    private void Update()
    {
        Movement();

        FireVulkan();
    }

    private void FireVulkan()
    {
        //Contador de la ametralladora
        _vulkanCounter += Time.deltaTime;

        //Si Espacio esta apretado y el contador es mayor a 1 sobre el firerate...
        if (Input.GetKey(KeyCode.Space) && _vulkanCounter > 1 / _vulkanFireRate)
        {
            //Se crea la bala en la posicion del objeto vulkanCannon
            Instantiate(_vulkanBullet, _vulkanCannon.transform.position, Quaternion.identity);
            //Se reinicia el contador para aplicar cooldown
            _vulkanCounter = 0;
        }
    }

    private void Movement()
    {
        //GetAxisRaw para que sea 0 o 1, numeros enteros. Con decimales el transform tarda en alcanzar la maxima velocidad
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        
        //Chequear si el jugador intenta salir de la pantalla
        CheckCameraBounds();

        //Crea vector director para saber a donde se mueve el jugador
        Vector3 _dir = new Vector3(_horizontal, _vertical).normalized;
        //Mueve el objeto a la direccion del jugador
        transform.Translate(_dir * _speed * Time.deltaTime);
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
    }

    private void CheckVulkanPoints()
    {
        int pointsExceed = 0;
        if (_currentVulkan > _nextVulkanPoints)
        {
            pointsExceed = _currentVulkan - _nextVulkanPoints;
            _currentVulkan = _nextVulkanPoints;
        }
        if (_currentVulkan == _nextVulkanPoints) _currentVulkanLevel++;
        _currentVulkan += pointsExceed;
        _nextVulkanPoints = _vulkanLevels[_currentVulkanLevel + 1];
        _pointsToNextVulkan = _nextVulkanPoints - _currentVulkan;
    }
}
