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
        SceneController.SwitchScene("Hall");
    }

    // Показ панели настроек
    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    // Выход из панели настроек
    public void BackToMenu()
    {
        startPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    // Выход из игры
    public void ExitGame()
    {
        Application.Quit();
    }
}
