using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Number_Fonts _numberFonts;
    [SerializeField] Image[] _moneyImages;
    [SerializeField] Image[] _scoreImages;

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
            Debug.Log(scoreDigits[i]);

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
}
