using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIGameplayManager : MonoBehaviour
{
    public static UIGameplayManager instance;
    [SerializeField] Number_Fonts _numberFonts;
    [SerializeField] Image[] _moneyImages;
    [SerializeField] Image[] _scoreImages;
    [SerializeField] Image healthBar;
    [SerializeField] float healthRatio;
    [SerializeField] Animator _healthBarAnim;
    [SerializeField] Animator _portraitAnim;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        EventBus.instance.OnPlayerSpawned += SetHealth;
        EventBus.instance.OnPlayerDamaged += UpdateHealthBar;
        EventBus.instance.OnPlayerDamaged += PlayPortraitHurt;
        EventBus.instance.OnPlayerRecover += PlayerRecovered;
    }

    private void OnDisable()
    {
        EventBus.instance.OnPlayerSpawned -= SetHealth;
        EventBus.instance.OnPlayerDamaged -= UpdateHealthBar;
        EventBus.instance.OnPlayerDamaged -= PlayPortraitHurt;
        EventBus.instance.OnPlayerRecover -= PlayerRecovered;
    }

    public void UpdateMoneySprites(int money)
    {
        int[] moneyDigits = GetIntArray(money);

        for (int i = 0; i < moneyDigits.Length; i++)
        {
            Debug.Log(moneyDigits[i]);

            if (!_moneyImages[i].gameObject.activeSelf) _moneyImages[i].gameObject.SetActive(true);
            _moneyImages[i].sprite = _numberFonts.sprite[moneyDigits[i]];
        }
    }

    public void UpdateScoreSprites(int score)
    {
        int[] scoreDigits = GetIntArray(score);

        for (int i = 0; i < scoreDigits.Length; i++)
        {
            //Debug.Log(scoreDigits[i]);

            if (!_scoreImages[i].gameObject.activeSelf) _scoreImages[i].gameObject.SetActive(true);
            _scoreImages[i].sprite = _numberFonts.sprite[scoreDigits[i]];
        }
    }

    int[] GetIntArray(int num)
    {
        List<int> list = new List<int>();
        while (num > 0)
        {
            list.Add(num % 10);
            num /= 10;
        }
        //list.Reverse();
        return list.ToArray();
    }

    public void UpdateHealthBar(float damage)
    {
        _healthBarAnim.SetBool("OnRecovery", true);
        healthBar.gameObject.SetActive(false);
        healthBar.fillAmount -= damage * healthRatio;
    }

    public void PlayPortraitHurt(float damage)
    {
        _portraitAnim.SetBool("OnDamaged", true);
    }

    public void PlayPortraitRecovery()
    {
        _portraitAnim.SetBool("OnDamaged", false);
        _portraitAnim.SetBool("OnRecovery", true);
    }

    public void SetHealth(PlayerController player)
    {
        healthRatio = 1 / player.maxHealth;
        healthBar.fillAmount = player.health / player.maxHealth;
    }

    public void PlayerRecovered()
    {
        _healthBarAnim.SetBool("OnRecovery", false);
        _healthBarAnim.SetBool("Recovered", true);
        _portraitAnim.SetBool("OnRecovery", false);
    }
    public void HealthBarBackToNormal()
    {
        _healthBarAnim.SetBool("Recovered", false);
        healthBar.gameObject.SetActive(true);
        StartCoroutine(RaiseHealth());
    }

    public IEnumerator RaiseHealth()
    {
        float currentHealth = healthBar.fillAmount/ healthRatio;
        healthBar.fillAmount = 0;
        for (int i = 0; i < currentHealth; i++)
        {
            Debug.Log(healthBar.fillAmount);
            healthBar.fillAmount = i * healthRatio;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
