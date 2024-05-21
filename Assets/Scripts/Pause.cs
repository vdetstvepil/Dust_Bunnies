using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR;


public class Pause : MonoBehaviour
{
    public GameObject pauseCanvas;

    public SteamVR_Action_Boolean menuAction;

    public SteamVR_Action_Vector2 touchpadAction;
    public SteamVR_Action_Boolean touchpadTouchAction;
    public GameObject[] buttons; // Массив кнопок

    public Sprite[] defaultSprites; // Массив спрайтов для обычного состояния
    public Sprite[] selectedSprites; // Массив спрайтов для выбранного состояния

    private int currentSelection = -1;
    private Image[] buttonImages;




    void Start()
    {
        pauseCanvas.SetActive(false);

        // Получение компонентов Image из кнопок
        buttonImages = new Image[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonImages[i] = buttons[i].GetComponent<Image>();
        }
    }

    void Update()
    {
        // Проверяем, нажата ли кнопка меню на контроллере
        if (menuAction.stateDown)
        {
            pauseCanvas.SetActive(true);
            Debug.Log("pause pushed");
            Time.timeScale = 0;
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
        float angle = Mathf.Atan2(touchPosition.y, touchPosition.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;

        if (angle >= 0 && angle < 120) return 0;
        if (angle >= 120 && angle < 240) return 1;
        return 2;
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
            buttonImages[currentSelection].sprite = selectedSprites[currentSelection];
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
        // Выполните действие, связанное с кнопкой index
        Debug.Log("Button " + index + " selected.");
    }


    // Нажатие на кнопку "Продолжить игру"
    public void ContinueGame()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

    // Показ панели настроек
    public void RestartGame()
    {

    }

    // Выход в меню
    public void ExitGame()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        SceneManager.LoadScene("Enter");

    }
}
