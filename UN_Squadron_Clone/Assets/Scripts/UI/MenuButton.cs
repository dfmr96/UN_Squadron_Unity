using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    [RequireComponent(typeof(AudioSource))]
    public class MenuButton : MonoBehaviour, ISelectHandler
    {
        [SerializeField] GameObject selector;
        [SerializeField] AudioSource selectAudio;

        private void OnEnable()
        {
            selectAudio = GetComponent<AudioSource>();
        }

        public void OnSelect(BaseEventData eventData)
        {
            selector.transform.position = this.transform.position;
            selectAudio.Play();
        }
    }
}
