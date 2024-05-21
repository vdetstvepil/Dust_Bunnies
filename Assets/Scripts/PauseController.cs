using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR;

public class PauseController : MonoBehaviour
{
    public GameObject pauseCanvas;

    public SteamVR_Action_Boolean menuAction;

    public SteamVR_Action_Vector2 touchpadAction;
    public SteamVR_Action_Boolean touchpadTouchAction;
    public GameObject[] buttons; // Массив кнопок

    public Sprite[] defaultSprites; // Массив спрайтов для обычного состояния
    public Sprite[] selectedSprites; // Массив спрайтов для выбранного состояния

    public float deadZoneRadius = 0.5f;

    private int currentSelection = -1;
    private Image[] buttonImages;
    private Button[] buttonComponents;

    void Start()
    {
        pauseCanvas.SetActive(false);

        // Получение компонентов Image из кнопок
        buttonImages = new Image[buttons.Length];
        buttonComponents = new Button[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonImages[i] = buttons[i].GetComponent<Image>();
            buttonComponents[i] = buttons[i].GetComponent<Button>();
        }
    }

    void Update()
    {
        // Проверяем, нажата ли кнопка меню на контроллере
        if (menuAction.stateDown)
        {
            if (SceneManager.GetActiveScene().name == "Start"
                || SceneManager.GetActiveScene().name == "Enter")
                return;

            pauseCanvas.SetActive(true);
            Debug.Log("pause pushed");
            StartCoroutine(SmoothTimeScaleAndFade(0, 0.5f));
        }

        if (Time.timeScale == 0)
        {
            if (touchpadTouchAction.GetState(SteamVR_Input_Sources.Any))
            {
                Vector2 touchPosition = touchpadAction.GetAxis(SteamVR_Input_Sources.Any);
                int newSelection = DetermineSelection(touchPosition);
                UpdateButtonSelection(newSelection);
            }
            else if (currentSelection != -1)
            {
                OnButtonSelected(currentSelection);
                ResetButtonSelection();
            }
        }
    }

    int DetermineSelection(Vector2 touchPosition)
    {
        if (touchPosition.magnitude < deadZoneRadius)
        {
            return -1;
        }

        float angle = Mathf.Atan2(touchPosition.y, touchPosition.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;

        if (angle >= 0 && angle < 120) return 0;
        if (angle >= 120 && angle < 240) return 2;
        return 1;
    }

    void UpdateButtonSelection(int newSelection)
    {
        if (newSelection != currentSelection)
        {
            if (currentSelection != -1)
            {
                buttonImages[currentSelection].sprite = defaultSprites[currentSelection];
            }

            currentSelection = newSelection;
            if (currentSelection != -1)
            {
                buttonImages[currentSelection].sprite = selectedSprites[currentSelection];
            }
        }
    }

    void ResetButtonSelection()
    {
        if (currentSelection != -1)
        {
            buttonImages[currentSelection].sprite = defaultSprites[currentSelection];
            currentSelection = -1;
        }
    }

    void OnButtonSelected(int index)
    {
        if (index != -1)
        {
            buttonComponents[index].onClick.Invoke();
        }
    }

    // Нажатие на кнопку "Продолжить игру"
    public void ContinueGame()
    {
        StartCoroutine(SmoothTimeScale(1, 0.5f));
        pauseCanvas.SetActive(false);
    }

    // Показ панели настроек
    public void RestartGame()
    {
        // Ваша логика перезапуска игры
    }

    // Выход в меню
    public void ExitGame()
    {
        StartCoroutine(SmoothTimeScale(1, 0.5f));
        pauseCanvas.SetActive(false);
        SceneController.SwitchScene("Enter");
    }

    // Корутин для плавной смены Time.timeScale
    IEnumerator SmoothTimeScale(float targetTimeScale, float duration)
    {
        float start = Time.timeScale;
        float t = 0;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(start, targetTimeScale, t / duration);
            yield return null;
        }
        Time.timeScale = targetTimeScale;
    }

    // Корутин для плавной смены Time.timeScale и мигания экрана
    IEnumerator SmoothTimeScaleAndFade(float targetTimeScale, float duration)
    {
        SteamVR_Fade.Start(Color.black * 0.2f, 0.25f); // Затемнение экрана
        yield return new WaitForSecondsRealtime(0.25f); // Ожидание 0.5 секунд
        SteamVR_Fade.Start(Color.clear, 0.25f); // Восстановление прозрачности

        yield return SmoothTimeScale(targetTimeScale, duration); // Плавная смена Time.timeScale
    }
}
