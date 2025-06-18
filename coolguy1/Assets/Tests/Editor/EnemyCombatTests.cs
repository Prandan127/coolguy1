#if UNITY_EDITOR
using NUnit.Framework;
using UnityEngine;
using TMPro;
using UnityEditor.Animations;

public class EnemyCombatTests
{
    private GameObject enemyGO;
    private EnemyCombat enemyCombat;
    private GameObject playerGO;
    private GameObject attackPointGO;
    private GameObject statsGO;
    private GameObject healthTextAnimGO;

    private AnimatorController CreateControllerWithState(string path, string stateName)
    {
        var controller = AnimatorController.CreateAnimatorControllerAtPath(path);
        var rootStateMachine = controller.layers[0].stateMachine;
        rootStateMachine.AddState(stateName);
        return controller;
    }

    [SetUp]
    public void Setup()
    {
        statsGO = new GameObject("StatsManager");
        var statsManager = statsGO.AddComponent<StatsManager>();
        statsManager.currentHealth = 100;
        statsManager.maxHealth = 100;

        var healthTextGO = new GameObject("HealthText");
        var tmpText = healthTextGO.AddComponent<TextMeshProUGUI>();
        statsManager.healthText = tmpText;

        StatsManager.Instance = statsManager;

        enemyGO = new GameObject("EnemyCombat");
        enemyCombat = enemyGO.AddComponent<EnemyCombat>();

        attackPointGO = new GameObject("AttackPoint");
        attackPointGO.transform.parent = enemyGO.transform;
        attackPointGO.transform.position = Vector3.zero;
        enemyCombat.attackPoint = attackPointGO.transform;

        enemyCombat.damage = 10;
        enemyCombat.weaponRange = 1.5f;
        enemyCombat.knockbackForce = 5f;
        enemyCombat.stunTime = 0.3f;
        enemyCombat.playerLayer = LayerMask.GetMask("Player");

        playerGO = new GameObject("Player");
        playerGO.layer = LayerMask.NameToLayer("Player");
        playerGO.transform.position = Vector3.zero;

        playerGO.AddComponent<Rigidbody2D>();

        var playerHealth = playerGO.AddComponent<PlayerHealth>();
        playerHealth.healthText = tmpText;

        healthTextAnimGO = new GameObject("HealthTextAnim");
        var healthTextAnimator = healthTextAnimGO.AddComponent<Animator>();
        var healthTextController = CreateControllerWithState("Assets/TempHealthTextController.controller", "TextUpdate");
        healthTextAnimator.runtimeAnimatorController = healthTextController;
        playerHealth.healthTextAnim = healthTextAnimator;

        PlayerHealth.Instance = playerHealth;

        var playerAnimator = playerGO.AddComponent<Animator>();
        var playerController = CreateControllerWithState("Assets/TempPlayerController.controller", "Idle");
        playerAnimator.runtimeAnimatorController = playerController;

        var playerMovement = playerGO.AddComponent<PlayerMovement>();
        playerMovement.rb = playerGO.GetComponent<Rigidbody2D>();

        playerGO.AddComponent<CircleCollider2D>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(enemyGO);
        Object.DestroyImmediate(playerGO);
        Object.DestroyImmediate(attackPointGO);
        Object.DestroyImmediate(statsGO);
        Object.DestroyImmediate(healthTextAnimGO);
        PlayerHealth.Instance = null;
        StatsManager.Instance = null;
    }

    [Test]
    public void Attack_ReducesPlayerHealth()
    {
        int healthBefore = StatsManager.Instance.currentHealth;

        enemyCombat.Attack();

        Assert.Less(StatsManager.Instance.currentHealth, healthBefore);
    }

    [Test]
    public void Attack_IncorrectlyIncreasesPlayerHealth()
    {
        int healthBefore = StatsManager.Instance.currentHealth;

        enemyCombat.Attack();

        Assert.Greater(StatsManager.Instance.currentHealth, healthBefore);
    }
}
#endif