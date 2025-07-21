using UnityEngine;
using System;

public class SoundsManager : MonoBehaviour
{
    [Header("Sounds")]
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

    private void GameStatusChangedCallback(GameManager.GameState gameState)
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

}
