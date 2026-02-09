using UnityEngine;

/*
PlatformerMotor2D

Ex. of usage:
    Side scrolling games
    Jump challenges

Requires ground check empty child!!!
*/

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementIntent2D))]
public class PlatformerMotor2D : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpForce = 12f;

    public float jumpBufferTime = 0.1f;
    public float airControlMultiplier = 0.7f;

    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private MovementIntent2D intent;

    private bool isGrounded;

    private float jumpBufferTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        intent = GetComponent<MovementIntent2D>();
    }

    void Update()
    {
        if (groundCheck == null)
        {
            enabled = false;
            Debug.LogError("GroundCheck missing!", this);
            return;
        }

        if (intent.JumpPressed)
            jumpBufferTimer = jumpBufferTime;
    }


    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        jumpBufferTimer -= Time.fixedDeltaTime;

        Vector2 velocity = rb.linearVelocity;

        float control = isGrounded ? 1f : airControlMultiplier;
        velocity.x = intent.MoveInput.x * moveSpeed * control;

        if (jumpBufferTimer > 0f && isGrounded)
        {
            velocity.y = jumpForce;
            jumpBufferTimer = 0f;
        }

        rb.linearVelocity = velocity;
    }


    void OnDrawGizmosSelected()
    {


        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}