using TMPro;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    public TMP_Text healthText;
    public Animator healthTextAnim;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        healthText.text = "HP: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHealth += amount;

        healthTextAnim.Play("TextUpdate");

        if (StatsManager.Instance.currentHealth <= 0)
        {
            PlayerRespawn.Instance.PlayerDeath();
        }

        healthText.text = "HP: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
    }
}