using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public PlayerCombat playerCombat;

    private int facingDirection = 1;
    private bool isKnockedBack;
    public bool isShooting;

    private void Update()
    {
        if (Input.GetButton("Attack") && playerCombat.enabled == true)
        {
            playerCombat.Attack();
        }
    }
    void FixedUpdate()
    {
        if (isShooting)
        {
#pragma warning disable CS0618 // Тип или член устарел
            rb.velocity = Vector2.zero;
#pragma warning restore CS0618 // Тип или член устарел
        }
        else if (!isKnockedBack)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0) Flip();

            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));

#pragma warning disable CS0618 // Тип или член устарел
            rb.velocity = new Vector3(horizontal, vertical) * StatsManager.Instance.speed;
#pragma warning restore CS0618 // Тип или член устарел
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void Knockback(Transform enemy, float force, float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
#pragma warning disable CS0618 // Тип или член устарел
        rb.velocity = direction * force;
#pragma warning restore CS0618 // Тип или член устарел
        StartCoroutine(KnockbackCounter(stunTime));
    }

    IEnumerator KnockbackCounter(float stumTime)
    {
        yield return new WaitForSeconds(stumTime);
#pragma warning disable CS0618 // Тип или член устарел
        rb.velocity = Vector2.zero;
#pragma warning restore CS0618 // Тип или член устарел
        isKnockedBack = false;
    }
}
