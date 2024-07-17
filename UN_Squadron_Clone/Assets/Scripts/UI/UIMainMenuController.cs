using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

namespace UI
{
    public class UIMainMenuController : MonoBehaviour
    {
        [SerializeField] VideoPlayer videoIntro;
        [SerializeField] GameObject mainTitle;
        [SerializeField] GameObject selector;
        [SerializeField] Button play;
        [SerializeField] AudioSource audioSource;
        [SerializeField] GameObject fader;
        [SerializeField] string sceneToLoad;

        private void Start()
        {
            videoIntro.gameObject.SetActive(true);
            mainTitle.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                videoIntro.gameObject.SetActive(false);
                if (!mainTitle.activeSelf)
                {
                    mainTitle.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(play.gameObject);
                    Debug.Log(EventSystem.current.currentSelectedGameObject.name);
                }
            }
        }

        public void PlayGame()
        {
            audioSource.Play();
            fader.GetComponent<Animator>().SetBool("GameStarted", true);
            selector.GetComponent<Animator>().SetBool("GameStarted", true);
            StartCoroutine(Play());
        }

        public IEnumerator Play()
        {
            yield return new WaitForSeconds(1f);
            LoadingManager.Instance.LoadNewScene(sceneToLoad);
        }
    }
}
