using UnityEngine;

public class EnemyShootAttack2D : EnemyAttackBase
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    public Transform target;
    public float range = 6f;

    protected override void Update()
    {
        base.Update();

        if (!Ready() || target == null)
            return;

        float dist = Vector2.Distance(transform.position, target.position);

        if (dist > range)
            return;

        Shoot();
        ResetCooldown();
    }

    void Shoot()
    {
        var go = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        var proj = go.GetComponent<Projectile>();
        if (proj != null)
            proj.SetTarget(target);
    }
}
