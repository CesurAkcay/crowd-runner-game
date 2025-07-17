using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Text levelText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressBar.value = 0f;

        gamePanel.SetActive(false);

        levelText.text = "Level " + (ChunkManager.instance.GetLevel() + 1);
    }

    // Update is called once per frame
    void Update()
    {
        updateProgressBar();
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
}
