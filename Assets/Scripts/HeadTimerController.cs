using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class HeadTimerController : MonoBehaviour
{
    public Image timerRing;  // Кольцо таймера
    public TextMeshProUGUI timerText;   // Текст для отображения времени
    public TextMeshProUGUI scoreText;   // Текст для отображения времени
    public Canvas canvas;    // Canvas, который будет отображаться или скрываться
    public float totalTime = 90f; // Полторы минуты
    public GameObject[] lives; // Массив жизней (иконок)
    private float timeRemaining;
    private bool isTimeUp = false;
    private int currentLives;
    private string currentSceneName;
    private GameKitchenModeData.GameMode currentGameMode;

    void Start()
    {
        timeRemaining = totalTime;
        currentLives = lives.Length;
        UpdateTimerText();
        Scene currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
        currentGameMode = GameKitchenModeData.CurrentGameMode;
        CheckScene(currentSceneName);

        scoreText.text = "0";
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != currentSceneName && currentGameMode != GameKitchenModeData.CurrentGameMode)
        {
            currentSceneName = currentScene.name;
            currentGameMode = GameKitchenModeData.CurrentGameMode;
            CheckScene(currentSceneName);
        }

        if (!isTimeUp && canvas.enabled)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining < 0)
                {
                    timeRemaining = 0;
                }
                UpdateTimerUI();
            }
            else
            {
                // Время вышло
                isTimeUp = true;
                StopTimer();
                timerText.color = Color.red;
            }
        }
    }

    void UpdateTimerUI()
    {
        timerRing.fillAmount = timeRemaining / totalTime;
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
            
    }

    void CheckScene(string sceneName)
    {
        if (sceneName == "Kitchen" 
            && GameKitchenModeData.CurrentGameMode == GameKitchenModeData.GameMode.Playing)
        {
            ResetTimer();
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }
    }

    public void ResetTimer()
    {
        timeRemaining = totalTime;
        isTimeUp = false;
        timerText.color = new Color32(101, 36, 129, 255);
        UpdateTimerUI();
    }

    public void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            lives[currentLives].SetActive(false);
            if (currentLives == 0)
            {
                StopTimer();
            }
        }
    }

    public void AddScore(int points)
    {
        GameKitchenModeData.Score += 100;
        scoreText.text = GameKitchenModeData.Score.ToString();
    }

    void StopTimer()
    {
        GameKitchenModeData.CurrentGameMode = GameKitchenModeData.GameMode.GameOver;
        SceneController.SwitchScene("Kitchen");

        isTimeUp = true;
        timeRemaining = 0;
        UpdateTimerUI();
        timerText.color = Color.red;

        
    }
}
