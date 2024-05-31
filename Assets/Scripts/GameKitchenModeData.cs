using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public static class GameKitchenModeData
{
    public enum GameMode
    {
        Start,
        Playing,
        GameOver
    }

    public static int Score { get; set; } = 0;
    public static GameMode CurrentGameMode;
}
