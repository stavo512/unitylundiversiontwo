using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public float maxHealth = 3f;
    public float Current { get; private set; }

    public event Action<float, Vector2> OnDamaged;
    public event Action OnDeath;

    void Awake()
    {
        Current = maxHealth;
    }

    public void TakeDamage(float damage, Vector2 hitPoint = default)
    {
        Current -= damage;

        OnDamaged?.Invoke(damage, hitPoint);

        if (Current <= 0f)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
