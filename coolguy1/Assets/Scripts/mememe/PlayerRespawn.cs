using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public static PlayerRespawn Instance;
    
    private Transform spawn;
    
    public CanvasGroup deathPanel;
    public Transform player;

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
        spawn = GameObject.FindWithTag("Spawn").GetComponent<Transform>();
    }
    
    public void PlayerDeath()
    {
        StatsManager.Instance.deaths++;
        InventoryManager.Instance.DropAllItems();
        SceneChanger.Instance.FadeToBlack();
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(1.5f);
        deathPanel.alpha = 1.0f;
        deathPanel.blocksRaycasts = true;
        Time.timeScale = 0;
    }

    public void Respawn()
    {
        Time.timeScale = 1;
        PlayerMovement.Instance.Knockback(spawn, 0, 0);
        player.position = spawn.position;
        StatsManager.Instance.currentHealth = StatsManager.Instance.maxHealth;
        PlayerHealth.Instance.healthText.text = "HP: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
        deathPanel.alpha = 0f;
        deathPanel.blocksRaycasts = false;
        SceneChanger.Instance.FadeFromBlack();
    }
}
