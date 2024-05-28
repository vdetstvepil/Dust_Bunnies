using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameKitchenModeManager : MonoBehaviour
{
    public enum GameMode
    {
        Start,
        Playing,
        GameOver
    }

    public static GameKitchenModeManager Instance;

    public GameMode CurrentGameMode { get; set; }
    public int Score { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetGameMode(GameMode mode)
    {
        CurrentGameMode = mode;
    }
}

