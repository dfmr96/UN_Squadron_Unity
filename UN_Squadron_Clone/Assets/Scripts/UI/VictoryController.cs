using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(BackToMainTitle());
    }

    public void LoadLevel()
    {
        string newLevel = GameManager.instance.CheckLevelToLoad();
        LoadingManager.Instance.LoadNewScene(newLevel);
    }
    
    public IEnumerator BackToMainTitle()
    {
        yield return new WaitForSeconds(10.0f);
        LoadingManager.Instance.LoadNewScene("Intro");
    }
}
