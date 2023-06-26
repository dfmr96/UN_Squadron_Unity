using System;
using System.Collections;
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
        EventBus.instance.OnBossDestroyed += BossDefeated;
    }

    private void OnDisable()
    {
        EventBus.instance.OnEnemyDestroyed -= UpdateMoney;
        EventBus.instance.OnEnemyDestroyed -= UpdateScore;
        EventBus.instance.OnBossDestroyed -= BossDefeated;

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

    public void BossDefeated()
    {
        
        StartCoroutine(GetBossMoney());
    }

    public IEnumerator GetBossMoney()
    {
        yield return new WaitForSeconds(2f);
        UIGameplayManager.instance._victoryPanel.SetActive(true);
        AudioManager.instance.bossReward.Play();
        Time.timeScale = 0f;
        while (money < 50000)
        {
            money += 1000;
            UIGameplayManager.instance.UpdateMoneySprites(money);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        LoadingManager.Instance.LoadNewScene("Victory");
    }
}
