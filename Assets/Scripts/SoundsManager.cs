using UnityEngine;
using System;

public class SoundsManager : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource doorHitSound;
    [SerializeField] private AudioSource runnerDieSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameOverSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerDetection.onDoorsHit += PlayHitDoorSound;

        GameManager.onGameStateChanged += GameStatusChangedCallback;

        Enemy.onRunnerDied += PlayRunnerDieSound;
    }

    private void OnDestroy()
    {
        PlayerDetection.onDoorsHit -= PlayHitDoorSound;

        GameManager.onGameStateChanged -= GameStatusChangedCallback;

        Enemy.onRunnerDied -= PlayRunnerDieSound;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayRunnerDieSound()
    {
        runnerDieSound.Play();
    }

    public void GameStatusChangedCallback(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.GameOver)
        {
            gameOverSound.Play();
        }
        else if (gameState == GameManager.GameState.LevelComplete)
        {
            levelCompleteSound.Play();
        }
    }

    private void PlayHitDoorSound()
    {
        doorHitSound.Play();
    }

    public void DisableSounds()
    {
        doorHitSound.volume = 0f;
        runnerDieSound.volume = 0f;
        levelCompleteSound.volume = 0f;
        gameOverSound.volume = 0f;
        buttonSound.volume = 0f;
    }

    public void EnableSounds()
    {
        doorHitSound.volume = 1f;
        runnerDieSound.volume = 1f;
        levelCompleteSound.volume = 1f;
        gameOverSound.volume = 1f;
        buttonSound.volume = 1f;
    }
}
