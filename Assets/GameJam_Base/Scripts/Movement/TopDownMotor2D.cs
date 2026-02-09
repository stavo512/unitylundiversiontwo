using UnityEngine;

/*
TopDownMotor2D

Ex. of usage:
    Shooters
    Puzzle
    Exploration

No gravity
360 movement
*/

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementIntent2D))]
public class TopDownMotor2D : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float acceleration = 15f;

    private Rigidbody2D rb;
    private MovementIntent2D intent;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        intent = GetComponent<MovementIntent2D>();

        rb.gravityScale = 0f;
    }

    void FixedUpdate()
    {
        Vector2 input = intent.MoveInput.normalized;

        Vector2 targetVelocity = input * moveSpeed;

        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, 1f - Mathf.Exp(-acceleration * Time.fixedDeltaTime));
    }
}
