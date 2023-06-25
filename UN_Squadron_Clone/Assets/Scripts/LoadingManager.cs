using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;
    [SerializeField] private string loadingSceneName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void LoadNewScene(string p_sceneName)
    {
        SceneManager.LoadScene(loadingSceneName);
        StartCoroutine(_LoadSceneCoroutine(p_sceneName));
    }

    private IEnumerator _LoadSceneCoroutine(string p_sceneName)
    {
        yield return new WaitForSeconds(0.1f);
        AsyncOperation l_asyncOperation;
        l_asyncOperation = SceneManager.LoadSceneAsync(p_sceneName);
        l_asyncOperation.allowSceneActivation = false;
        while (!l_asyncOperation.isDone && l_asyncOperation.progress < 0.8)
        {
            Debug.Log($"Cargando al {l_asyncOperation.progress * 100}%");
            yield return null;
        }
        Debug.Log("Carga terminada!! Presione una tecla para continuar...");
        //yield return new WaitUntil(() => Input.anyKey);
        l_asyncOperation.allowSceneActivation = true;
    }
}
