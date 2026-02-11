using UnityEngine;

public class EndGameOnTrigger : MonoBehaviour
{
    public enum Result { Win, Lose }

[Header("Who can trigger")]
    public LayerMask triggeringLayers;

    [Header("Result")]
    public Result result = Result.Win;

    [Header("Optional")]
    public bool disableAfterTrigger = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((triggeringLayers.value & (1 << other.gameObject.layer)) == 0)
            return;

        if (GameManager.Instance == null)
            return;

        if (result == Result.Win)
            GameManager.Instance.WinGame();
        else
            GameManager.Instance.LoseGame();

        if (disableAfterTrigger)
            gameObject.SetActive(false);
    }

}
