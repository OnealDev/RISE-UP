using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject gameOverPanel; 
    public Button restartButton;
    public Button quitButton;

    private bool isGameOverShown = false;

    void Start()
    {
        // Hide the panel at start
        gameOverPanel.SetActive(false);

        // Assign button click events
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void ShowGameOver()
    {
        if (isGameOverShown) return; // Only show once
        isGameOverShown = true;

        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    void RestartGame()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void QuitGame()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene("Main Menu");   // Make sure this matches the scene name exactly
       
        Debug.Log("Quit Game"); 
    }
}
