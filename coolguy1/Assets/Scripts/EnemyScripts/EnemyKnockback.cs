using System.Collections;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
    }
    public void Knockback(Transform playerTransform, float knockbackForce, float knockbackTime,float stunTime)
    {
        enemyMovement.ChangeState(EnemyState.Knockback);
        StartCoroutine(StunTimer(knockbackTime, stunTime));
        Vector2 direction = (transform.position- playerTransform.position).normalized;
#pragma warning disable CS0618 // Тип или член устарел
        rb.velocity = direction * knockbackForce;
#pragma warning restore CS0618 // Тип или член устарел
    }

    IEnumerator StunTimer(float knockbackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockbackTime);
#pragma warning disable CS0618 // Тип или член устарел
        rb.velocity = Vector2.zero;
#pragma warning restore CS0618 // Тип или член устарел
        yield return new WaitForSeconds(stunTime);
        enemyMovement.ChangeState(EnemyState.Idle);
    }
}
