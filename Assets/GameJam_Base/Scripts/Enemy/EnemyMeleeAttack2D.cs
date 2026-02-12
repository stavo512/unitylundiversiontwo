using UnityEngine;

public class EnemyMeleeAttack2D : EnemyAttackBase
{
    public DamageGiver damageZone;
    public float attackDuration = 0.2f;
    public float range = 1f;

    public Transform target;

    float activeTimer;

    protected override void Update()
    {
        base.Update();

        if (target == null)
            return;

        if (activeTimer > 0f)
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer <= 0f)
                damageZone.gameObject.SetActive(false);

            return;
        }

        float dist = Vector2.Distance(transform.position, target.position);

        if (dist <= range && Ready())
        {
            damageZone.gameObject.SetActive(true);
            activeTimer = attackDuration;
            ResetCooldown();
        }
    }
}
