using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public static EventBus instance;
    public event Action<int, int> OnPOWTaken;

    public event Action<Enemy> OnEnemyDestroyed;
    public event Action<float> OnPlayerDamaged;
    public event Action OnPlayerDestroyed;
    public event Action OnPlayerRecover;
    public event Action<PlayerController> OnPlayerSpawned;
    public event Action OnBossDestroyed;
    public event Action<float> OnSubweaponUsed;
    public event Action<WeaponData> OnSubweaponChanged;



    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (GameManager.instance != null)
            {
                GameManager.instance.SetEventBus(this);
            }
            Debug.Log("EventBus Instance Created");
        } else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.UnsuscribeToCurrentEventBus();
    }

    public void PowTaken(int remaining, int total)
    {
        OnPOWTaken?.Invoke(remaining, total);
    }

    public void SubWeaponChanged(WeaponData weaponData)
    {
        OnSubweaponChanged?.Invoke(weaponData);
    }

    public void SubWeaponUsed(float remaining)
    {
        OnSubweaponUsed?.Invoke(remaining);
    }

    public void EnemyDestroyed(Enemy enemy)
    {
        OnEnemyDestroyed?.Invoke(enemy);
    }

    public void PlayerDamaged(float damage)
    {
        OnPlayerDamaged?.Invoke(damage);
    }

    public void PlayerDestroyed()
    {
        OnPlayerDestroyed?.Invoke();
    }

    public void PlayerSpawned(PlayerController player)
    {
        OnPlayerSpawned?.Invoke(player);
    }

    public void PlayerRecovered()
    {
        OnPlayerRecover?.Invoke();
    }
    public void BossDestroyed()
    {
        OnBossDestroyed?.Invoke();
    }
}
