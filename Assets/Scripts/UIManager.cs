using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private Slider progressBar;
    [SerializeField] private Text levelText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressBar.value = 0f;

        gamePanel.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        settingsPanel.SetActive(false);

        levelText.text = "Level " + (ChunkManager.instance.GetLevel() + 1);

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;

    }

   


    // Update is called once per frame
    void Update()
    {
        updateProgressBar();
    }

    private void GameStateChangedCallback(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.GameOver)
        {
            ShowGameOver();
        }
        else if (gameState == GameManager.GameState.LevelComplete)
        {
            ShowLevelComplete();
        }
    }

    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowGameOver()
    {

        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    private void ShowLevelComplete()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void updateProgressBar() 
    {
        if(!GameManager.instance.IsGameState())
            return;

        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishPosition();
        progressBar.value = progress;
    }

    public void ShowsSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void HideSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }
}
