using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager instance; //singleton instance of GameManager
    public enum GameState { Menu, Game, LevelComplete, GameOver}

    private GameState gameState;

    public static Action<GameState> onGameStateChanged;  // Action to notify when the game state changes. Action will message the game state to all listeners


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
    void Start()
    {
        PlayerPrefs.DeleteAll(); // Clear PlayerPrefs for testing purposes
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState; // we will be able to call this method from anywhere in the game
        onGameStateChanged?.Invoke(gameState); // Notify all listeners that the game state has changed

        Debug.Log("Game State Changed to: " + gameState); 
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game; // Check if the current game state is Game
    }
}
