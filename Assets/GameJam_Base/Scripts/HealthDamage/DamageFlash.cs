using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Health))]
public class DamageFlash : MonoBehaviour
{
    public Color flashColor = Color.red;
    public float flashTime = 0.08f;

    SpriteRenderer sr;
    Color original;
    float timer;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        original = sr.color;

        GetComponent<Health>().OnDamaged += OnDamaged;
    }

    void OnDamaged(float dmg, Vector2 hit)
    {
        sr.color = flashColor;
        timer = flashTime;
    }

    void Update()
    {
        if (timer <= 0f) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
            sr.color = original;
    }
}
