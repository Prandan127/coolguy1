using NUnit.Framework;
using UnityEngine;
using UnityEditor.Animations;

public class PlayerCombatTests
{
    private GameObject playerGO;
    private PlayerCombat playerCombat;
    private GameObject statsGO;
    private GameObject attackPointGO;

    [SetUp]
    public void Setup()
    {
        statsGO = new GameObject("StatsManager");
        var statsManager = statsGO.AddComponent<StatsManager>();
        statsManager.damage = 10;
        statsManager.weaponRange = 1.5f;
        statsManager.cooldown = 1f;
        statsManager.knockbackForce = 5f;
        statsManager.knockbackTime = 0.5f;
        statsManager.stunTime = 0.3f;
        StatsManager.Instance = statsManager;

        playerGO = new GameObject("PlayerCombat");
        playerCombat = playerGO.AddComponent<PlayerCombat>();

        attackPointGO = new GameObject("AttackPoint");
        attackPointGO.transform.position = Vector3.zero;
        playerCombat.attackPoint = attackPointGO.transform;

        var animatorGO = new GameObject("Animator");
        animatorGO.transform.parent = playerGO.transform;
        var animator = animatorGO.AddComponent<Animator>();
        var controller = AnimatorController.CreateAnimatorControllerAtPath("Assets/TempTestController.controller");
        controller.AddParameter("isAttacking", AnimatorControllerParameterType.Bool);
        animator.runtimeAnimatorController = controller;
        playerCombat.anim = animator;

        playerCombat.enemyLayer = LayerMask.GetMask("Enemy");
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(playerGO);
        Object.DestroyImmediate(statsGO);
        Object.DestroyImmediate(attackPointGO);
        StatsManager.Instance = null;
    }

    [Test]
    public void Attack_SetsIsAttackingTrue_And_ResetsTimer()
    {
        playerCombat.anim.SetBool("isAttacking", false);

        playerCombat.Attack();

        Assert.IsTrue(playerCombat.anim.GetBool("isAttacking"));

        var timerField = typeof(PlayerCombat).GetField("timer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        float timerValue = (float)timerField.GetValue(playerCombat);
        Assert.AreEqual(StatsManager.Instance.cooldown, timerValue);
    }

    [Test]
    public void FinishAttaking_SetsIsAttackingFalse()
    {
        playerCombat.anim.SetBool("isAttacking", true);

        playerCombat.FinishAttaking();

        Assert.IsFalse(playerCombat.anim.GetBool("isAttacking"));
    }
}