using UnityEngine;

public class FollowMovement2D : EnemyMovementBase
{
    public Transform target;
    public float speed = 3f;

    public enum MovementPlane
    {
        HorizontalOnly,
        VerticalOnly,
        TopDown
    }

    [Header("Movement")]
    public MovementPlane plane = MovementPlane.HorizontalOnly;

    void FixedUpdate()
    {
        if (!canMove || target == null)
            return;

        Vector2 dir = (target.position - transform.position).normalized;
        Vector2 velocity = Vector2.zero;

        switch (plane)
        {
            case MovementPlane.HorizontalOnly:
                velocity = new Vector2(dir.x, 0f);
                break;

            case MovementPlane.VerticalOnly:
                velocity = new Vector2(0f, dir.y);
                break;

            case MovementPlane.TopDown:
                velocity = dir;
                break;
        }

        rb.linearVelocity = velocity * speed;
    }
}
