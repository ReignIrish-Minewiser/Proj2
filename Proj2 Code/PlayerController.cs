using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask platformLayer;
    private LayerMask combinedLayer;
    public Camera mainCamera;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private bool isFalling;
    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;
    public float fallThreshold = -10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        combinedLayer = groundLayer | platformLayer;

        if (anim == null)
        {
            Debug.LogError("Animator component missing from Player!");
        }
    }


    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, combinedLayer);

        if (GameManager.isGameOver)
    {
        rb.velocity = Vector2.zero;
        if (anim != null)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isGrounded", true);
        }
        Destroy(gameObject);
        return;
    }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            gameObject.tag = "PlayerJump";
        }
        if (isGrounded)
        {
            gameObject.tag = "Player";
        }

        if (anim != null)
        {
            anim.SetBool("isWalking", move != 0);
            anim.SetBool("isGrounded", isGrounded);
            anim.SetFloat("velocityY", rb.velocity.y);
        }

        if (move != 0)
        {
            transform.localScale = new Vector3(move < 0 ? -1 : 1, 1, 1);
        }
        if (currentPlatform != null)
    {
        Vector3 platformMovement = currentPlatform.position - lastPlatformPosition;
        transform.position += platformMovement;
        lastPlatformPosition = currentPlatform.position;
    }
    if (transform.position.y < fallThreshold)
    {
        GameManager.instance.GameOver();
    }
    }

private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("MovingPlatform"))
    {
        currentPlatform = collision.transform;
        lastPlatformPosition = currentPlatform.position;
    }
    if (collision.gameObject.CompareTag("Enemy"))
    {
            Destroy(collision.gameObject);
    }
}
private void OnCollisionExit2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("MovingPlatform"))
    {
        currentPlatform = null;
    }
}

    void LateUpdate()
    {
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }
    public void TakeDamage()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
         GameManager.instance.GameOver();
    }
}
