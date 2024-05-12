using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR;


public class MenuController : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject startPanel;

    // Переход на сцену "Enter"
    public void StartGame()
    {
        SceneController.SwitchScene("Enter");
    }

    // Показ панели настроек
    public void ShowSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
            startPanel.SetActive(false);
        }
    }

    // Выход из игры
    public void ExitGame()
    {
        Application.Quit();
    }
}
