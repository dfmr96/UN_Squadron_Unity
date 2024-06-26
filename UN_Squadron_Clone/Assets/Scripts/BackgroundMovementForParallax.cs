using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovementForParallax : MonoBehaviour
{
    [SerializeField] private Vector2 movementSpeed;
    private Vector2 _offset;
    private Material _material;

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        _offset = movementSpeed * Time.deltaTime;
        _material.mainTextureOffset += _offset;
    }
}
