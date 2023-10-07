using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header("Music Symbol Text")]
    [SerializeField] private Text[] mysicSymbolText;
    private int musicS;

    private void Awake()
    {
        musicS = PlayerPrefs.GetInt("music", 0);

        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

    }
    void Start()
    {
        AddCoins(5);
        UpadeteMusicScore();
    }


    private void UpadeteMusicScore()
    {
        foreach(Text musicText in mysicSymbolText)
        {
            musicText.text = musicS.ToString();
        }
    }
    public void AddCoins(int amount)
    {
        musicS += amount;

        UpadeteMusicScore();

        PlayerPrefs.SetInt("music", musicS);
    }
    public int GetCoins()
    {
        return musicS;
    }
    public void UseCoins(int amount)
    {
        musicS -= amount;
        UpadeteMusicScore();
        PlayerPrefs.SetInt("music", musicS);
    }
}
