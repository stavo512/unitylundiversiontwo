using UnityEngine;

public class EndGameOnDeath : MonoBehaviour
{
    public enum Result { Win, Lose }
    public Result result = Result.Lose;

    Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void OnEnable()
    {
        if (health != null)
            health.OnDeath += HandleDeath;
    }

    void OnDisable()
    {
        if (health != null)
            health.OnDeath -= HandleDeath;
    }

    void HandleDeath()
    {
        if (GameManager.Instance == null)
            return;

        if (result == Result.Win)
            GameManager.Instance.WinGame();
        else
            GameManager.Instance.LoseGame();
    }
}
