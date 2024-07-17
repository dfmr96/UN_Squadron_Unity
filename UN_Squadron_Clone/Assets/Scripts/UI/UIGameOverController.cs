using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOverController : MonoBehaviour
{
    [SerializeField] Animator _fader;
    [SerializeField] private string levelToLoad;

    private void Start()
    {
        StartCoroutine(BackToMainTitle());
    }
    public IEnumerator BackToMainTitle()
    {
        _fader.GetComponent<Animator>().SetBool("GameOver", true);
        yield return new WaitForSeconds(4f);
        LoadingManager.Instance.LoadNewScene(levelToLoad);
    }
}
