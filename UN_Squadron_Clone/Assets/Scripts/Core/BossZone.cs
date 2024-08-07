using Player;
using UnityEngine;

namespace Core
{
    public class BossZone : MonoBehaviour
    {
        [SerializeField] private GameObject boss;
        [SerializeField] private SideScrollController sideScroll;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerController>() != null)
            {
                AudioManager.instance.bgmAudio.Stop();
                AudioManager.instance.bossBGM.Play();
                GetComponent<Collider2D>().enabled = false;
                if (boss != null)
                {
                    sideScroll.PauseScroll();
                    boss.SetActive(true);
                }
            }
        }
    }
}
