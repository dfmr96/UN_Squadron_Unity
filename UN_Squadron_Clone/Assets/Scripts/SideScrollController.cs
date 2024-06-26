using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollController : MonoBehaviour
{
    [SerializeField] private float speed;
    [field: SerializeField] public BoxCollider2D Col { get; private set; }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }
}
