using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
