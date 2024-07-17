using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
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
                yield return null;
            }
            l_asyncOperation.allowSceneActivation = true;
        }
    }
}
