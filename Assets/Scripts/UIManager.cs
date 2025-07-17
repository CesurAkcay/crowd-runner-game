using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Text levelText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressBar.value = 0f;

        gamePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        levelText.text = "Level " + (ChunkManager.instance.GetLevel() + 1);
        
        // Subscribe to game state changes
        GameManager.onGameStateChanged += OnGameStateChanged;
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from game state changes
        GameManager.onGameStateChanged -= OnGameStateChanged;
    }
    
    private void OnGameStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Menu:
                menuPanel.SetActive(true);
                gamePanel.SetActive(false);
                if (gameOverPanel != null) gameOverPanel.SetActive(false);
                break;
            case GameManager.GameState.Game:
                menuPanel.SetActive(false);
                gamePanel.SetActive(true);
                if (gameOverPanel != null) gameOverPanel.SetActive(false);
                break;
            case GameManager.GameState.GameOver:
                menuPanel.SetActive(false);
                gamePanel.SetActive(false);
                if (gameOverPanel != null) gameOverPanel.SetActive(true);
                break;
            case GameManager.GameState.LevelComplete:
                // Handle level complete if needed
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateProgressBar();
    }

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);
    }
    
    public void RestartButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void MenuButtonPressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Menu);
    }

    public void updateProgressBar() 
    {
        if(!GameManager.instance.IsGameState())
            return;

        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishPosition();
        progressBar.value = progress;
    }
}
