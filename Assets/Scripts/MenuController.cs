using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public GameObject settingsPanel; // Панель настроек
    public GameObject startPanel;

    // Метод для перехода на сцену "SampleScene"
    public void StartGame()
    {
        // Показываем экран загрузки
        //loadingScreen.SetActive(true);

        // Выгружаем текущую сцену из памяти
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        Destroy(GameObject.Find("Player"));

        // Асинхронно загружаем сцену "Enter"
        StartCoroutine(LoadSceneAsync("Enter"));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Пока сцена загружается, обновляем прогресс загрузки
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // Прогресс загрузки от 0 до 1
           // loadingBar.value = progress; // Обновляем значение прогресс-бара
            yield return null;
        }
    }


    // Метод для показа панели настроек
    public void ShowSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
            startPanel.SetActive(false);
        }
    }

    // Метод для выхода из игры
    public void ExitGame()
    {
        Application.Quit();
    }
}
