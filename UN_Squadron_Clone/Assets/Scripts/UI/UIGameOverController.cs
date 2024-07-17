using System.Collections;
using Core;
using UnityEngine;

namespace UI
{
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
}
