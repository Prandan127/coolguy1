using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTests
{
    private GameObject playerGO;
    private PlayerMovement playerMovement;

    [SetUp]
    public void Setup()
    {
        playerGO = new GameObject();
        playerMovement = playerGO.AddComponent<PlayerMovement>();
        playerGO.transform.localScale = Vector3.one;
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(playerGO);
    }
    
    [Test]
    public void Flip_ChangesLocalScaleX()
    {
        float initialScaleX = playerGO.transform.localScale.x;

        playerMovement.Flip();

        Assert.AreEqual(-initialScaleX, playerGO.transform.localScale.x);
        Assert.AreEqual(-1, playerMovement.facingDirection);
    }
}
