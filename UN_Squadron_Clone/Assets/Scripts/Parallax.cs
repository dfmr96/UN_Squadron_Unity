using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] SpriteRenderer backgroundLayer;
    [SerializeField] float backgroundSpeed;
    [SerializeField] SpriteRenderer middlegroundLayer;
    [SerializeField] float middlegroundSpeed;
    [SerializeField] SpriteRenderer foregroundLayer;
    [SerializeField] float foregroundSpeed;
    [SerializeField] SideScrollController _camController;

    private void LateUpdate()
    {
        backgroundLayer.gameObject.transform.Translate(Vector3.right * (backgroundSpeed * Time.deltaTime));
        middlegroundLayer.gameObject.transform.Translate(Vector3.right * (middlegroundSpeed * Time.deltaTime));
    }
}
