using NUnit.Framework;
using UnityEngine;
using TMPro;
using Assets.Scripts.Interfaces;

public class PlayerHealthTests
{
    private GameObject playerGO;
    private PlayerHealth playerHealth;
    private GameObject statsGO;
    private GameObject respawnGO;
    private PlayerRespawnMock mockRespawn;

    public class PlayerRespawnMock : MonoBehaviour, IPlayerRespawn
    {
        public bool DeathCalled { get; private set; }

        public void PlayerDeath()
        {
            DeathCalled = true;
        }
    }

    [SetUp]
    public void Setup()
    {
        statsGO = new GameObject("StatsManager");
        var statsManager = statsGO.AddComponent<StatsManager>();
        statsManager.maxHealth = 100;
        statsManager.currentHealth = 100;
        StatsManager.Instance = statsManager;

        respawnGO = new GameObject("PlayerRespawn");
        mockRespawn = respawnGO.AddComponent<PlayerRespawnMock>();
        PlayerRespawn.Instance = mockRespawn;

        playerGO = new GameObject("PlayerHealth");
        playerHealth = playerGO.AddComponent<PlayerHealth>();

        var textGO = new GameObject("HealthText");
        textGO.transform.parent = playerGO.transform;
        var tmpText = textGO.AddComponent<TextMeshPro>();
        playerHealth.healthText = tmpText;

        var animatorGO = new GameObject("Animator");
        animatorGO.transform.parent = playerGO.transform;
        var animator = animatorGO.AddComponent<Animator>();
        playerHealth.healthTextAnim = animator;
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(playerGO);
        Object.DestroyImmediate(statsGO);
        Object.DestroyImmediate(respawnGO);
        StatsManager.Instance = null;
        PlayerRespawn.Instance = null;
    }

    [Test]
    public void ChangeHealth_UpdatesHealthText()
    {
        playerHealth.healthText.text = "HP: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
        Assert.AreEqual("HP: 100 / 100", playerHealth.healthText.text);

        playerHealth.ChangeHealth(-30);

        Assert.AreEqual("HP: 70 / 100", playerHealth.healthText.text);
    }

    [Test]
    public void ChangeHealth_TriggersPlayerDeath_WhenHealthZeroOrLess()
    {
        playerHealth.ChangeHealth(-100);

        Assert.IsTrue(mockRespawn.DeathCalled);
    }
}