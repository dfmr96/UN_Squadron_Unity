using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SubWeaponSelector : MonoBehaviour
{
    private GameObject _selected;
    private RectTransform _rectTransform;
    [SerializeField] private float _speed;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        var selectedGameObject = EventSystem.current.currentSelectedGameObject;
        _selected = (selectedGameObject == null) ? _selected : selectedGameObject;
        EventSystem.current.SetSelectedGameObject(_selected);
        if (_selected == null) return;

        //transform.position = Vector3.Lerp(transform.position, _selected.transform.position, _speed * Time.deltaTime);
        transform.position = _selected.transform.position;
        //var otherRect = _selected.GetComponent<RectTransform>();

        //var horizontalLerp = Mathf.Lerp(_rectTransform.rect.size.x, otherRect.rect.size.x, _speed * Time.deltaTime);
        //var verticalLerp = Mathf.Lerp(_rectTransform.rect.size.y, otherRect.rect.size.y, _speed * Time.deltaTime);


        //_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, horizontalLerp);
        //_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, verticalLerp);
    }
}
