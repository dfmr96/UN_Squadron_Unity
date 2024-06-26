using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadNewScene());
    }

    public void LoadLevel()
    {
        string newLevel = GameManager.instance.CheckLevelToLoad();
        LoadingManager.Instance.LoadNewScene(newLevel);
    }
    
    public IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(10.0f);
        LoadLevel();
        //LoadingManager.Instance.LoadNewScene("Intro");
    }
}
