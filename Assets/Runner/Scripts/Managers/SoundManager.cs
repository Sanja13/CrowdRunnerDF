using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource doorHitSound;
    [SerializeField] private AudioSource runnerDieSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameoverSound;
    [SerializeField] private AudioSource backgroundSound;
    void Start()
    {
        backgroundSound.Play();

        PlayerDetection.onDoorsHit += PlayDoorHitSound;

        GameManager.onGameStateChange += GameStateChangedCallBack;

        Enemy.onRunnerDied += PlayRunnerDieSound;
    }
    private void OnDestroy()
    {
        PlayerDetection.onDoorsHit -= PlayDoorHitSound;
        GameManager.onGameStateChange -= GameStateChangedCallBack;
        Enemy.onRunnerDied -= PlayRunnerDieSound;
    }

    
    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.LevelComplete)
            levelCompleteSound.Play();
        else if (gameState == GameManager.GameState.Gameover)
            gameoverSound.Play();
    }
    private void PlayDoorHitSound()
    {
        doorHitSound.Play();
    }
    private void PlayRunnerDieSound()
    {
        runnerDieSound.Play();
    }
    public void DisableSound()
    {
        doorHitSound.volume = 0;
        runnerDieSound.volume = 0;
        levelCompleteSound.volume = 0;
        gameoverSound.volume = 0;
        buttonSound.volume= 0;
        backgroundSound.volume = 0;
    }
    public void EnableSound()
    {
        doorHitSound.volume = 1;
        runnerDieSound.volume =1;
        levelCompleteSound.volume = 1;
        gameoverSound.volume = 1;
        buttonSound.volume = 1;
        backgroundSound.volume = 1;
    }
}
