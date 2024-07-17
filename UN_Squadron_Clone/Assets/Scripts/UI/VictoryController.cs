using System.Collections;
using Core;
using UnityEngine;

namespace UI
{
    public class VictoryController : MonoBehaviour
    {
        [SerializeField] private string levelToLoad;
        private void Start()
        {
            StartCoroutine(LoadNewScene());
        }

        public void LoadLevel()
        {
            LoadingManager.Instance.LoadNewScene(levelToLoad);
        }
    
        public IEnumerator LoadNewScene()
        {
            yield return new WaitForSeconds(10.0f);
            LoadLevel();
            //LoadingManager.Instance.LoadNewScene("Intro");
        }
    }
}
