using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyMovementBase : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected bool canMove = true;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetMovementEnabled(bool enabled)
    {
        canMove = enabled;

        if (!enabled)
            rb.linearVelocity = Vector2.zero;
    }
}
