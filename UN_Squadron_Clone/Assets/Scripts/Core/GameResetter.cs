using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class GameResetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetLevels();
            
        }
    }
}