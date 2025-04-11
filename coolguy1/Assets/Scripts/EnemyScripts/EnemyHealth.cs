using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    public bool isBoss;
    public int expReward = 3;
    public delegate void MonsterDefeat(int exp);
    public static event MonsterDefeat OnMonsterDefeat;

    public int currentHealth;
    public int maxHealth;

    public event Action OnDeath;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        OnMonsterDefeat?.Invoke(expReward);
        OnDeath?.Invoke();
        Destroy(gameObject);
        StatsManager.Instance.kills++;
    }
}