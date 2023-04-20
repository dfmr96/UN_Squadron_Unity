using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
