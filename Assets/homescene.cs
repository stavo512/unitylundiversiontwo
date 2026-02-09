using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenController : MonoBehaviour
{
    public string gameSceneName = "iltombe"; // Change this to your game scene name
    
    void Update()
    {
        // Press Enter to start the game
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }
}