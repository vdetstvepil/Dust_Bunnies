using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HandTimerController : MonoBehaviour
{
    public Image timerRing;  // Кольцо таймера
    public TextMeshProUGUI timerText;   // Текст для отображения времени
    public Canvas canvas;    // Canvas, который будет отображаться или скрываться
    public float totalTime = 90f; // Полторы минуты
    private float timeRemaining;
    private bool isTimeUp = false;
    private string currentSceneName;

    void Start()
    {
        timeRemaining = totalTime;
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
        if (sceneName == "Kitchen")
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
        timerText.color = Color.black; // Или любой другой начальный цвет
        UpdateTimerUI();
    }
}
