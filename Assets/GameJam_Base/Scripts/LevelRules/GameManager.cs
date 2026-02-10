using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        Won,
        Lost
    }

    public enum EndBehaviour { ShowScreen, RestartLevel, LoadScene }

    [Header("UI Panels")]
    public GameObject mainMenuScreen;
    public GameObject pauseScreen;
    public GameObject winScreen;
    public GameObject loseScreen;

    [Header("Win Behaviour")]
    public EndBehaviour winBehaviour = EndBehaviour.ShowScreen;
    public string winSceneName;

    [Header("Lose Behaviour")]
    public EndBehaviour loseBehaviour = EndBehaviour.RestartLevel;
    public string loseSceneName;

    [Header("Optional Timer")]
    public bool useTimer;
    public float levelTime = 60f;
    public TMP_Text timerText;
    public bool timerEndsInWin = false;

    public GameState CurrentState { get; private set; }

    float currentTime;

    // Setup


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Multiple GameManagers in scene! Delete duplicates.", this);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        HideAllScreens();

        currentTime = levelTime;

        if (mainMenuScreen)
            SetState(GameState.MainMenu);
        else
            SetState(GameState.Playing);
    }

    // Update

    void Update()
    {
        HandlePauseInput();

        if (CurrentState != GameState.Playing)
            return;

        HandleTimer();
    }

    void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentState == GameState.Playing)
                Pause();
            else if (CurrentState == GameState.Paused)
                Resume();
        }
    }

    // Timer

    void HandleTimer()
    {
        if (!useTimer) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            if (timerEndsInWin)
                WinGame();
            else
                LoseGame();

            return;
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (!timerText) return;

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    // State Control

    void SetState(GameState newState)
    {
        CurrentState = newState;

        HideAllScreens();

        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 0f;
                if (mainMenuScreen) mainMenuScreen.SetActive(true);
                break;

            case GameState.Playing:
                Time.timeScale = 1f;
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                if (pauseScreen) pauseScreen.SetActive(true);
                break;

            case GameState.Won:
                Time.timeScale = 0f;
                break;

            case GameState.Lost:
                Time.timeScale = 0f;
                break;
        }
    }

    void HideAllScreens()
    {
        if (mainMenuScreen) mainMenuScreen.SetActive(false);
        if (pauseScreen) pauseScreen.SetActive(false);
        if (winScreen) winScreen.SetActive(false);
        if (loseScreen) loseScreen.SetActive(false);
    }

    // Public API (Buttons can call these)

    public void Play() => SetState(GameState.Playing);

    public void Pause() => SetState(GameState.Paused);

    public void Resume() => SetState(GameState.Playing);

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Win / Lose

    public void WinGame()
    {
        if (CurrentState != GameState.Playing) return;

        SetState(GameState.Won);
        HandleEnd(winBehaviour, winScreen, winSceneName);
    }

    public void LoseGame()
    {
        if (CurrentState != GameState.Playing) return;

        SetState(GameState.Lost);
        HandleEnd(loseBehaviour, loseScreen, loseSceneName);
    }

    void HandleEnd(EndBehaviour behaviour, GameObject screen, string sceneName)
    {
        switch (behaviour)
        {
            case EndBehaviour.ShowScreen:
                if (screen) screen.SetActive(true);
                break;

            case EndBehaviour.RestartLevel:
                RestartGame();
                break;

            case EndBehaviour.LoadScene:
                LoadScene(sceneName);
                break;
        }
    }

    void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName);
    }

    public float GetRemainingTime() => currentTime;
}
