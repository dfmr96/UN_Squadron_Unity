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
    public AudioSource playerUnableToRecover;
    public AudioSource bossBGM;
    public AudioSource bossMisiles;
    public AudioSource bossDestroyed;
    public AudioSource bossReward;

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
