using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            AudioManager.instance.bgmAudio.Stop();
            AudioManager.instance.bossBGM.Play();
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
