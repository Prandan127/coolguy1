using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int expReward = 3;
    public delegate void MonsterDefeat(int exp);
    public static event MonsterDefeat OnMonsterDefeat;

    public int currentHealth;
    public int maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = amount;
        }
        else if (currentHealth <= 0)
        { 
            OnMonsterDefeat(expReward);
            Destroy(gameObject);
        }
    }
}
