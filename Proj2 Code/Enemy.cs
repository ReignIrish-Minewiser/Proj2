using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform leftPoint, rightPoint;
    private Rigidbody2D rb;
    private Animator anim;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float moveDirection = movingRight ? 1 : -1;
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        if ((movingRight && transform.position.x >= rightPoint.position.x) ||
            (!movingRight && transform.position.x <= leftPoint.position.x))
        {
            Flip();
        }

        if (anim != null)
        {
            anim.SetBool("isMoving", Mathf.Abs(rb.velocity.x) > 0.1f);
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        transform.localScale = new Vector3(movingRight ? 1 : -1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerController player = collision.collider.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerJump"))
        {
            Destroy(gameObject);
            GameManager.instance.EnemyDefeated();
        }
    }
}
