using Enemies.Core;
using ScriptableObjects.Inventory;
using ScriptableObjects.Subweapons;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
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
        [field: SerializeField] public UIGameplayManager GameplayManager { get; private set; }
        [SerializeField] private UIStoreManager uiStoreManager;
        [field: SerializeField] public EventBus EventBus { get; private set; }
        public void SetUIGameplayManager(UIGameplayManager uiGameplayManager)
        {
            GameplayManager = uiGameplayManager;
        }
        public void SetUIStoreManager(UIStoreManager uiStoreManager)
        {
            this.uiStoreManager = uiStoreManager;
        }
        public void SetEventBus(EventBus eventBus)
        {
            EventBus = eventBus;
            EventBus.OnEnemyDestroyed += UpdateMoney;
            EventBus.OnEnemyDestroyed += UpdateScore;
        }
        public void UnsuscribeToCurrentEventBus()
        {
            if (EventBus == null) return;
            EventBus.OnEnemyDestroyed -= UpdateMoney;
            EventBus.OnEnemyDestroyed -= UpdateScore;
            EventBus = null;
        }
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
        public void SetLevelCompleted(int i)
        {
            levelCompleted[i] = true;
        }
        public void UpdateMoney(Enemy enemy)
        {
            Money += enemy.moneyPerKill;
            if (GameplayManager == null) return;
            GameplayManager.UpdateMoneySprites(Money);
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

        private void UpdateScore(Enemy enemy)
        {
            Score += enemy.scorePerKill;
            if (GameplayManager == null) return;
            GameplayManager.UpdateScoreSprites(Score);
        }
    
        public void UpdateScore(int score)
        {
            Score += score;
            GameplayManager.UpdateScoreSprites(Score);
        }

        public void GameOver()
        {
            LoadingManager.Instance.LoadNewScene("GameOver");
        }
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
}
