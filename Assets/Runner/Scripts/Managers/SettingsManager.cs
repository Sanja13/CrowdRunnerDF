using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Elements")]
    
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Image soundButtonImage;
    [SerializeField] private Sprite turnOnSprite;
    [SerializeField] private Sprite turnOffSprite;

    [Header("Settings")]
    private bool soundState=true;
    private void Awake()
    {
        soundState = PlayerPrefs.GetInt("sounds", 1) == 1;
    }

    void Start()
    {
        SetUP();
    }
    private void SetUP()
    {
        if (soundState)
            EnableSound();
        else
            DisableSounds();
    }
    public void ChangeSound()
    {
        if (soundState)
            DisableSounds();
        else
            EnableSound();

        soundState = !soundState;
        PlayerPrefs.SetInt("sounds", soundState ? 1 : 0);
    }
    private void DisableSounds()
    {
        soundManager.DisableSound();
        soundButtonImage.sprite = turnOffSprite;
    }
    private void EnableSound()
    {
        soundManager.EnableSound();
        soundButtonImage.sprite = turnOnSprite;
    }
}
