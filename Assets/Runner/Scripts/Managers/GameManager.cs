using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState { Menu,Game, LevelComplete,Gameover}

    private GameState gameState;

    public static Action<GameState> onGameStateChange;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }
    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChange?.Invoke(gameState);
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
