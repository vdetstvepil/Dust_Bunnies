using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneKitchenSetup : MonoBehaviour
{
    public Canvas GameCanvas;
    public GameObject StartPanel;
    public GameObject GameOverPanel;
    public GameObject Room;

    void Start()
    {
        GameKitchenModeData.GameMode currentMode = GameKitchenModeData.CurrentGameMode;
        
        if (GameKitchenModeData.CurrentGameMode == GameKitchenModeData.GameMode.Playing)
        {
            ((HeadTimerController)FindObjectOfType<HeadTimerController>()).canvas.enabled = true;
            ((HeadTimerController)FindObjectOfType<HeadTimerController>()).ResetTimer();
        }
        else
        {
            ((HeadTimerController)FindObjectOfType<HeadTimerController>()).canvas.enabled = false;
        }

        switch (currentMode)
        {
            case GameKitchenModeData.GameMode.Start:
                EnableGameCanvas();
                EnableStartPanel();
                DisableEnemiesAndBunny();
                Room.transform.localScale = Vector3.one;
                break;

            case GameKitchenModeData.GameMode.Playing:
                GameKitchenModeData.Score = 0;
                break;

            case GameKitchenModeData.GameMode.GameOver:
                EnableGameCanvas();
                EnableGameOverPanel();
                DisableEnemiesAndBunny();
                Room.transform.localScale = Vector3.one;
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
        GameKitchenModeData.CurrentGameMode = GameKitchenModeData.GameMode.Playing;
        SceneController.SwitchScene("Kitchen");
    }


    void Update()
    {
        
    }
}
