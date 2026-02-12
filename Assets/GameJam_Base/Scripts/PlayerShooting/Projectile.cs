using UnityEngine;

/*
Movement only.

Supports:
- straight
- homing
- limited lifetime
- variable size/scale

ToBeAttached:
- Rigidbody2D (kinematic recommended)
- Collider2D (is trigger)
- DamageGiver (optional)
*/

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public enum MoveType
    {
        Straight,
        Homing
    }

    [Header("Movement")]
    public MoveType moveType = MoveType.Straight;
    public float speed = 10f;
    public bool randomizeSpeed = false;
    public float minSpeed = 5f;
    public float maxSpeed = 15f;

    [Header("Homing (optional)")]
    public Transform target;
    public float turnSpeed = 360f;

    [Header("Lifetime")]
    public float lifetime = 3f;

    [Header("Size")]
    public bool randomizeSize = false;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Apply random size if enabled
        if (randomizeSize)
        {
            float randomScale = Random.Range(minSize, maxSize);
            transform.localScale = Vector3.one * randomScale;
        }

        // Apply random speed if enabled
        if (randomizeSpeed)
        {
            speed = Random.Range(minSpeed, maxSpeed);
        }

        Destroy(gameObject, lifetime);

        // initial velocity for straight shots
        if (moveType == MoveType.Straight)
            rb.linearVelocity = transform.up * speed;
    }

    void FixedUpdate()
    {
        switch (moveType)
        {
            case MoveType.Straight:
                
                break;

            case MoveType.Homing:
                UpdateHoming();
                break;
        }
    }

    // HOMING LOGIC
    void UpdateHoming()
    {
        if (target == null)
        {
            // fallback to straight
            rb.linearVelocity = transform.up * speed;
            return;
        }

        Vector2 toTarget = (target.position - transform.position).normalized;

        float angle = Vector2.SignedAngle(transform.up, toTarget);

        float maxStep = turnSpeed * Time.fixedDeltaTime;

        float step = Mathf.Clamp(angle, -maxStep, maxStep);

        transform.Rotate(0f, 0f, step);

        rb.linearVelocity = transform.up * speed;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Set specific size when spawning
    public void SetSize(float size)
    {
        transform.localScale = Vector3.one * size;
    }

    // Set size with separate x and y values
    public void SetSize(float width, float height)
    {
        transform.localScale = new Vector3(width, height, 1f);
    }

    // Set specific speed when spawning
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        if (moveType == MoveType.Straight)
            rb.linearVelocity = transform.up * speed;
    }
}