using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class VibrationManager : MonoBehaviour
{
    [Header("Settings")]
    private bool haptics;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerDetection.onDoorsHit += Vibrate;
        Enemy.onRunnerDied += Vibrate;

        GameManager.onGameStateChanged += GameStatusChangedCallback;

    }
    private void OnDestroy()
    {
        PlayerDetection.onDoorsHit += Vibrate;
        Enemy.onRunnerDied += Vibrate;

        GameManager.onGameStateChanged -= GameStatusChangedCallback;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStatusChangedCallback(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.GameOver)
        {
            Vibrate();
        }
        else if (gameState == GameManager.GameState.LevelComplete)
        {
            Vibrate();
        }
    }
    private void Vibrate() 
    {
        if (haptics) 
        {
            Taptic.Light();
        }
        
    }

    public void DisableVibrations()
    {
        haptics = false;
    }

    public void EnableVibrations()
    {
        haptics = true;
    }

}
