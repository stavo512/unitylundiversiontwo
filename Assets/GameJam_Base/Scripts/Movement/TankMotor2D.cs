using UnityEngine;

/*
TankMotor2D

Ex. of usage:
    Vehicles
    Character driven shooters
    Stylized controls

X = rotation
Y = move forward/back
*/

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementIntent2D))]
public class TankMotor2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 200f;
    public float acceleration = 10f;

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
        float turn = -intent.MoveInput.x * turnSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + turn);

        float forwardSpeed = Vector2.Dot(rb.linearVelocity, transform.up);

        Vector2 forwardVelocity = transform.up * forwardSpeed;

        Vector2 targetVelocity = transform.up * intent.MoveInput.y * moveSpeed;

        rb.linearVelocity = Vector2.Lerp(forwardVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);

    }
}
