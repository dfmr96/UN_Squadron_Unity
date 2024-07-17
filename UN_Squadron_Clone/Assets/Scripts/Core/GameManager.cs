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

    [SerializeField] private bool[] levelCompleted = new bool[2];

    [SerializeField] private string[] levelSceneNames = new string[2];

    [SerializeField] private string[] sceneNames = new string[2];

    [SerializeField] private UIGameplayManager uiGameplayManager;

    [SerializeField] private UIStoreManager uiStoreManager;
    //public event Action OnGameOver;

    public void SetUIGameplayManager(UIGameplayManager uiGameplayManager)
    {
        this.uiGameplayManager = uiGameplayManager;
    }
    
    public void SetUIStoreManager(UIStoreManager uiStoreManager)
    {
        this.uiStoreManager = uiStoreManager;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        Score = 0;
        Money = 3000;
        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().name == "Shop") playerInventory.slots.Clear();
    }

    public void SetLevelCompleted(int i)
    {
        levelCompleted[i] = true;
    }

    private void OnEnable()
    {
        if (EventBus.instance != null)
        {
            EventBus.instance.OnEnemyDestroyed += UpdateMoney;
            EventBus.instance.OnEnemyDestroyed += UpdateScore;
            //EventBus.instance.OnBossDestroyed += BossDefeated;
        }
    }

    private void OnDisable()
    {
        if (EventBus.instance != null)
        {
            EventBus.instance.OnEnemyDestroyed -= UpdateMoney;
            EventBus.instance.OnEnemyDestroyed -= UpdateScore;
            //EventBus.instance.OnBossDestroyed -= BossDefeated;
        }

    }

    public void UpdateMoney(Enemy enemy)
    {
        Money += enemy.moneyPerKill;
        uiGameplayManager.UpdateMoneySprites(Money);
    }

    public void RemoveMoney(WeaponData weaponData)
    {
        Money -= weaponData.price;
        if (uiStoreManager == null) return;
        uiStoreManager.UpdateMoneySprites(Money);
    }

    public void AddMoney(WeaponData weaponData)
    {
        Money += weaponData.price;
        if (uiStoreManager == null) return;
        uiStoreManager.UpdateMoneySprites(Money);
    }

    public void AddMoney(int moneyToAdd)
    {
        Money += moneyToAdd;
        if (uiStoreManager == null) return;
        uiStoreManager.UpdateMoneySprites(Money);
    }

    public void UpdateScore(Enemy enemy)
    {
        Score += enemy.scorePerKill;
        uiGameplayManager.UpdateScoreSprites(Score);
    }
    
    public void UpdateScore(int score)
    {
        Score += score;
        uiGameplayManager.UpdateScoreSprites(Score);
    }

    public void GameOver()
    {
        LoadingManager.Instance.LoadNewScene("GameOver");
    }

    /*public void BossDefeated()
    {
        StartCoroutine(GetBossMoney());
    }

    public IEnumerator GetBossMoney()
    {
        Debug.Log("GetBoosMoney");
        yield return new WaitForSeconds(2f);
        uiGameplayManager._victoryPanel.SetActive(true);
        AudioManager.instance.bossReward.Play();
        Time.timeScale = 0f;
        int moneyGranted = 0;
        while (moneyGranted < 50000)
        {
            moneyGranted += 1000;
            Money += 1000;
            uiGameplayManager.UpdateMoneySprites(Money);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        LoadingManager.Instance.LoadNewScene($"{CheckSceneToLoad()}");
    }*/

    public string CheckSceneToLoad()
    {
        for (int i = 0; i < levelCompleted.Length; i++)
        {
            if (levelCompleted[i] == false)
            {
                return sceneNames[i - 1];
            }
        }

        return null;
    }
    
    public string CheckLevelToLoad()
    {
        for (int i = 0; i < levelCompleted.Length; i++)
        {
            if (levelCompleted[i] == false)
            {
                return levelSceneNames[i];
            }
        }

        return null;
    }

    public void LoadNextScene()
    {
        LoadingManager.Instance.LoadNewScene($"{CheckSceneToLoad()}");
    }

    public void SaveVulkanPoints(int points)
    {
        VulkanPoints = points;
    }

    public void ResetLevels()
    {
        for (int i = 0; i < levelCompleted.Length; i++)
        {
            levelCompleted[i] = false;
        }

        Money = 0;
        VulkanPoints = 0;
        Score = 0;
    }
}
