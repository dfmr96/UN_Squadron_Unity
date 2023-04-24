using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource bgmAudio;
    public AudioSource vulkanAudio;
    public AudioSource enemyDestroyedAudio;
    public AudioSource playerDamaged;
    public AudioSource playerDestroyed;
    public AudioSource playerRecovery;
    public AudioSource vulkanPOW;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
}
