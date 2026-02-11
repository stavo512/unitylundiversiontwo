using UnityEngine;

public class EndGameWhenAllCollected : MonoBehaviour
{
    public enum Result { Win, Lose }
    public Result result = Result.Win;

bool triggered;

    void Update()
    {
        if (triggered)
            return;

        if (CollectibleCounter.Remaining > 0)
            return;

        if (GameManager.Instance == null)
            return;

        triggered = true;

        if (result == Result.Win)
            GameManager.Instance.WinGame();
        else
            GameManager.Instance.LoseGame();
    }

}
