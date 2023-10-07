using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private ShopManager shopManager;

    [Header("Elemnts")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCopletePanel;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject shopPanel;

    [SerializeField] private Slider progressBar;
    [SerializeField] private Text levelText;

    void Start()
    {
        progressBar.value = 0;
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        HideShop();

        levelText.text = "Level " + ChunkManager.instance.GelLevel() + 1;

        GameManager.onGameStateChange += GameStateChangedCallBack;


    }
    private void OnDestroy()
    {
        GameManager.onGameStateChange -= GameStateChangedCallBack;
    }


    void Update()
    {
        UpdateProgressBar();
    }
    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.Gameover)
            ShowGameOver();
        else if (gameState == GameManager.GameState.LevelComplete)
            ShowLevelComplete();

    }
    public void PlayButtonPresed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }
    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
    private void ShowLevelComplete()
    {
        gamePanel.SetActive(false);
        levelCopletePanel.SetActive(true);
    }

    public void ShowGameOver()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
    public void UpdateProgressBar()
    {
        if (GameManager.instance.IsGameState())
        { 
            float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetZposition();
            progressBar.value = progress;

        }
       
    }
    public void ShowSettingsPanel()
    {
        SettingsPanel.SetActive(true);
    }
    public void HideSettingsPanel()
    {
        SettingsPanel.SetActive(false);
    }
    public void ShowShop()
    {
        shopPanel.SetActive(true);
        shopManager.UpdatePurchaseButton();
    }
    public void HideShop()
    {
        shopPanel.SetActive(false);
    }
}
