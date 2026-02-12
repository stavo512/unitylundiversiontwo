using UnityEngine;

public class PatrolMovement2D : EnemyMovementBase
{
    public Transform pointA;
    public Transform pointB;

    public float speed = 2f;
    public float waitTime = 0f;

    public enum PatrolMode
    {
        HorizontalOnly,
        VerticalOnly,
        BetweenPoints
    }

    [Header("Movement")]
    public PatrolMode mode = PatrolMode.HorizontalOnly;

    Transform target;
    float waitTimer;

    protected override void Awake()
    {
        base.Awake();
        target = pointB;
    }

    void Update()
    {
        if (!canMove || pointA == null || pointB == null)
            return;

        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 pos = rb.position;
        Vector2 targetPos = target.position;
        Vector2 velocity = Vector2.zero;

        switch (mode)
        {
            case PatrolMode.HorizontalOnly:
                velocity = new Vector2(Mathf.Sign(targetPos.x - pos.x) * speed, 0f);

                // arrival check only on X
                if (Mathf.Abs(targetPos.x - pos.x) < 0.05f)
                    SwitchTarget();
                break;

            case PatrolMode.VerticalOnly:
                velocity = new Vector2(0f, Mathf.Sign(targetPos.y - pos.y) * speed);

                // arrival check only on Y
                if (Mathf.Abs(targetPos.y - pos.y) < 0.05f)
                    SwitchTarget();
                break;

            case PatrolMode.BetweenPoints:
                Vector2 dir = (targetPos - pos).normalized;
                velocity = dir * speed;

                if (Vector2.Distance(pos, targetPos) < 0.05f)
                    SwitchTarget();
                break;
        }

        rb.linearVelocity = velocity;
    }

    void SwitchTarget()
    {
        target = target == pointA ? pointB : pointA;
        waitTimer = waitTime;
    }
}
