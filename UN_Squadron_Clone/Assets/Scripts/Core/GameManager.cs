using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [field: SerializeField] public int Score { get; private set; }
    [field: SerializeField] public int Money { get; private set; }
    
    [field: SerializeField] public int VulkanPoints { get; private set; }

    [SerializeField] private Inventory playerInventory;
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

        Score = 0;
        Money = 3000;
        DontDestroyOnLoad(gameObject);
        
        if (SceneManager.GetActiveScene().name == "Shop") playerInventory.slots.Clear();
    }

    private void OnEnable()
    {
        if (EventBus.instance != null)
        {
            EventBus.instance.OnEnemyDestroyed += UpdateMoney;
            EventBus.instance.OnEnemyDestroyed += UpdateScore;
            EventBus.instance.OnBossDestroyed += BossDefeated;
        }
    }

    private void OnDisable()
    {
        if (EventBus.instance != null)
        {
            EventBus.instance.OnEnemyDestroyed -= UpdateMoney;
            EventBus.instance.OnEnemyDestroyed -= UpdateScore;
            EventBus.instance.OnBossDestroyed -= BossDefeated;
        }

    }

    public void UpdateMoney(Enemy enemy)
    {
        Money += enemy.moneyPerKill;
        UIGameplayManager.instance.UpdateMoneySprites(Money);
    }

    public void RemoveMoney(WeaponData weaponData)
    {
        Money -= weaponData.price;
        UIStoreManager.instance.UpdateMoneySprites(Money);
    }

    public void AddMoney(WeaponData weaponData)
    {
        Money += weaponData.price;
        UIStoreManager.instance.UpdateMoneySprites(Money);
    }

    public void AddMoney(int moneyToAdd)
    {
        Money += moneyToAdd;
        UIStoreManager.instance.UpdateMoneySprites(Money);
    }

    public void UpdateScore(Enemy enemy)
    {
        Score += enemy.scorePerKill;
        UIGameplayManager.instance.UpdateScoreSprites(Score);
    }
    
    public void UpdateScore(int score)
    {
        Score += score;
        UIGameplayManager.instance.UpdateScoreSprites(Score);
    }

    public void GameOver()
    {
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
        int moneyGranted = 0;
        while (moneyGranted < 50000)
        {
            moneyGranted += 1000;
            Money += 1000;
            UIGameplayManager.instance.UpdateMoneySprites(Money);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        LoadingManager.Instance.LoadNewScene("Victory");
    }

    public void SaveVulkanPoints(int points)
    {
        VulkanPoints = points;
    }
}
