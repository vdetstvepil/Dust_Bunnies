using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class HandTimerController : MonoBehaviour
{
    public Image timerRing;  // Кольцо таймера
    public TextMeshProUGUI timerText;   // Текст для отображения времени
    public Canvas canvas;    // Canvas, который будет отображаться или скрываться
    public float totalTime = 90f; // Полторы минуты
    public GameObject[] lives; // Массив жизней (иконок)
    private float timeRemaining;
    private bool isTimeUp = false;
    private int currentLives;
    private string currentSceneName;

    void Start()
    {
        timeRemaining = totalTime;
        currentLives = lives.Length; // Изначально количество жизней равно длине массива
        UpdateTimerText();
        Scene currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
        CheckScene(currentSceneName);
    }

    void Update()
    {
        // Проверка текущей сцены
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != currentSceneName)
        {
            currentSceneName = currentScene.name;
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
                if (timeRemaining - 15 % 15 == 0)
                    GameKitchenModeManager.Instance.Score += 10;
            }
            else
            {
                // Время вышло
                isTimeUp = true;
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
        if (sceneName == "Kitchen" && GameKitchenModeManager.Instance.CurrentGameMode == GameKitchenModeManager.GameMode.Playing)
        {
            ResetTimer();
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }
    }

    void ResetTimer()
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
            lives[currentLives].SetActive(false); // Делаем иконку невидимой
            if (currentLives == 0)
            {
                StopTimer();
            }
        }
    }

    void StopTimer()
    {
        isTimeUp = true;
        timeRemaining = 0;
        UpdateTimerUI();
        timerText.color = Color.red;

        SceneController.SwitchScene("Kitchen");
        GameKitchenModeManager.Instance.SetGameMode(GameKitchenModeManager.GameMode.Start);
    }
}
