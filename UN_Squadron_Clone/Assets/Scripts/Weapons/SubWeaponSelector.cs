using UnityEngine;
using UnityEngine.EventSystems;

namespace Weapons
{
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

            transform.position = _selected.transform.position;
        }
    }
}
