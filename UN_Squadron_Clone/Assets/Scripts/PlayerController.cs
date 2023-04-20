using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 15;
    [SerializeField] GameObject _camera;
    [SerializeField] float _horizontal;
    [SerializeField] float _vertical;


    [SerializeField] GameObject _vulkanBullet;
    [SerializeField] GameObject _vulkanCannon;
    [SerializeField] float _vulkanFireRate;
    [SerializeField] float vulkanCounter;
    private BoxCollider2D _cameraCol;
    private BoxCollider2D _playerCol;
    private void Start()
    {
        _camera = FindObjectOfType<CameraController>().gameObject;
        _cameraCol = _camera.GetComponentInChildren<BoxCollider2D>();
        _playerCol = GetComponent<BoxCollider2D>();
        transform.parent = _camera.transform;
    }

    
    private void Update()
    {
        Movement();

        FireVulkan();
    }

    private void FireVulkan()
    {
        vulkanCounter += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && vulkanCounter > 1 / _vulkanFireRate)
        {
            Instantiate(_vulkanBullet, _vulkanCannon.transform.position, Quaternion.identity);
            vulkanCounter = 0;
        }
    }

    private void Movement()
    {
        //GetAxisRaw para que sea 0 o 1, numeros enteros. Con decimales el transform tarda en alcanzar la maxima velocidad
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        
        CheckCameraBounds();

        Vector3 _dir = new Vector3(_horizontal, _vertical).normalized;
        transform.Translate(_dir * _speed * Time.deltaTime);
    }

    private void CheckCameraBounds()
    {
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
}
