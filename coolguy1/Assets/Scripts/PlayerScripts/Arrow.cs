using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float lifeSpawn = 2f;
    public float speed;

    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;

    public SpriteRenderer sr;
    public Sprite buriedSprite;

    public int damage;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;
    void Start()
    {
#pragma warning disable CS0618 // Тип или член устарел
        rb.velocity = direction * speed;
#pragma warning restore CS0618 // Тип или член устарел
        RotateArrow();
        Destroy(gameObject,lifeSpawn);
    }
    
    private void RotateArrow()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

#pragma warning disable UNT0006 // Incorrect message signature
    public void OnCollisionEnter2D(Collision2D collision)
#pragma warning restore UNT0006 // Incorrect message signature
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) >  0)
        {
            collision.gameObject.GetComponent<EnemyHealth>().ChangeHealth(-damage);
            collision.gameObject.GetComponent<EnemyKnockback>().Knockback(transform, knockbackForce, knockbackTime, stunTime);
            AttachToTarget(collision.gameObject.transform);
        }
        else if ((obstacleLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            AttachToTarget(collision.gameObject.transform);
        }
    }

    private void AttachToTarget(Transform target)
    {
        sr.sprite = buriedSprite;

#pragma warning disable CS0618 // Тип или член устарел
        rb.velocity = Vector2.zero;
#pragma warning restore CS0618 // Тип или член устарел
#pragma warning disable CS0618 // Тип или член устарел
        rb.isKinematic = true;
#pragma warning restore CS0618 // Тип или член устарел

        transform.SetParent(target);
    }
}
