using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score;
    public int money;
    //public event Action OnGameOver;

    private void Awake()
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
    private void Start()
    {
        
        score = 0;
        money = 0;
    }

    private void OnEnable()
    {
        EventBus.instance.OnEnemyDestroyed += UpdateMoney;
        EventBus.instance.OnEnemyDestroyed += UpdateScore;
    }

    private void OnDisable()
    {
        EventBus.instance.OnEnemyDestroyed -= UpdateMoney;
        EventBus.instance.OnEnemyDestroyed -= UpdateScore;
    }

    public void UpdateMoney(Enemy enemy)
    {
        money += enemy.moneyPerKill;
        UIGameplayManager.instance.UpdateMoneySprites(money);
    }

    public void UpdateScore(Enemy enemy)
    {
        score += enemy.scorePerKill;
        UIGameplayManager.instance.UpdateScoreSprites(score);
    }

    public void GameOver()
    {
        //OnGameOver?.Invoke();
        LoadingManager.Instance.LoadNewScene("GameOver");
    }
}
