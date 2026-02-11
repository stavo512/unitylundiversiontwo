using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartHealthUI : MonoBehaviour
{
    [Header("Target")]
    public Health targetHealth;

    [Header("UI")]
    public Image heartPrefab;
    public Transform container;

    [Header("Sprites")]
    public Sprite fullHeart;
    public Sprite emptyHeart;

    List<Image> hearts = new();

    void Start()
    {
        if (targetHealth == null || heartPrefab == null || container == null)
        {
            Debug.LogError("HeartHealthUI: Missing references", this);
            enabled = false;
            return;
        }

        BuildHearts();

        targetHealth.OnDamaged += OnHealthChanged;
        targetHealth.OnDeath += OnHealthChangedInstant;

        Refresh();
    }

    void OnDestroy()
    {
        if (targetHealth == null) return;

        targetHealth.OnDamaged -= OnHealthChanged;
        targetHealth.OnDeath -= OnHealthChangedInstant;
    }

 

    void BuildHearts()
    {
        hearts.Clear();

        int count = Mathf.CeilToInt(targetHealth.maxHealth);

        for (int i = 0; i < count; i++)
        {
            Image heart = Instantiate(heartPrefab, container);
            hearts.Add(heart);
        }
    }

    void OnHealthChanged(float dmg, Vector2 hit)
    {
        Refresh();
    }

    void OnHealthChangedInstant()
    {
        Refresh();
    }

    void Refresh()
    {
        int current = Mathf.CeilToInt(targetHealth.Current);

        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].sprite = i < current ? fullHeart : emptyHeart;
        }
    }
}
