using NUnit.Framework;
using UnityEngine;
using TMPro;
using UnityEditor.Animations;

public class PlayerHealthTests
{
    private GameObject playerGO;
    private PlayerHealth playerHealth;
    private GameObject statsGO;
    private GameObject respawnGO;

    public class PlayerRespawnMock : MonoBehaviour
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
        statsManager.currentHealth = 100;
        statsManager.maxHealth = 100;
        StatsManager.Instance = statsManager;

        respawnGO = new GameObject("Respawn");
        var mockRespawn = respawnGO.AddComponent<PlayerRespawnMock>();
        PlayerRespawn.Instance = null;
        PlayerRespawn.Instance = respawnGO.AddComponent<PlayerRespawn>();
        respawnGO.AddComponent<PlayerRespawnMock>();

        playerGO = new GameObject("PlayerHealth");
        playerHealth = playerGO.AddComponent<PlayerHealth>();

        var textGO = new GameObject("HealthText");
        textGO.transform.parent = playerGO.transform;
        var tmpText = textGO.AddComponent<TextMeshPro>();
        playerHealth.healthText = tmpText;

        var animatorGO = new GameObject("Animator");
        animatorGO.transform.parent = playerGO.transform;
        var animator = animatorGO.AddComponent<Animator>();

        var controller = AnimatorController.CreateAnimatorControllerAtPathWithClip("Assets/Temp.controller", null);
        animator.runtimeAnimatorController = controller;

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
        var mockRespawn = respawnGO.GetComponent<PlayerRespawnMock>();

        playerHealth.ChangeHealth(-100);

        Assert.IsTrue(mockRespawn.DeathCalled);
    }
}