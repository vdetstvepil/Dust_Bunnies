using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneKitchenSetup : MonoBehaviour
{
    public Canvas GameCanvas;
    public GameObject StartPanel;
    public GameObject GameOverPanel;

    void Start()
    {
        GameKitchenModeManager.GameMode currentMode = GameKitchenModeManager.Instance.CurrentGameMode;

        switch (currentMode)
        {
            case GameKitchenModeManager.GameMode.Start:
                EnableGameCanvas();
                EnableStartPanel();
                DisableEnemiesAndBunny();
                break;

            case GameKitchenModeManager.GameMode.Playing:
                GameKitchenModeManager.Instance.Score = 0;
                break;

            case GameKitchenModeManager.GameMode.GameOver:
                EnableGameCanvas();
                EnableGameOverPanel();
                DisableEnemiesAndBunny();
                break;
        }

    }
    public void DisableEnemiesAndBunny()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }

        GameObject bunny = GameObject.Find("Bunny");
        if (bunny != null)
        {
            bunny.SetActive(false);
        }
    }

    public void EnableGameCanvas()
    {
        if (GameCanvas != null)
        {
            GameCanvas.gameObject.SetActive(true);
            StartPanel.SetActive(true);
            StartPanel.SetActive(true);
        }
    }

    public void EnableStartPanel()
    {
        if (StartPanel != null)
        {
            StartPanel.SetActive(true);
        }
    }

    public void EnableGameOverPanel()
    {
        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(true);
        }
    }

    public void BeginGame()
    {
        SceneController.SwitchScene("Kitchen");
        GameKitchenModeManager.Instance.SetGameMode(GameKitchenModeManager.GameMode.Playing);
    }


    void Update()
    {
        
    }
}
