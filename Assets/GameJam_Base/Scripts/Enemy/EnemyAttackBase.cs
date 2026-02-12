using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{
    public float cooldown = 1f;

    protected float timer;

    protected virtual void Update()
    {
        timer -= Time.deltaTime;
    }

    protected bool Ready()
    {
        return timer <= 0f;
    }

    protected void ResetCooldown()
    {
        timer = cooldown;
    }
}
