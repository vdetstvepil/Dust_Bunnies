using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class SceneController : MonoBehaviour
{
    // Метод для переключения сцен
    public static void SwitchScene(string sceneName)
    {
        // Затемняем экран
        SteamVR_Fade.Start(Color.black, 3f);

        // Загружаем новую сцену асинхронно
        Instance.StartCoroutine(LoadSceneAsync(sceneName));

        if (sceneName != "Start")
        {
            GameObject[] hands = GameObject.FindGameObjectsWithTag("HandsVR");

            foreach (GameObject hand in hands)
            {
                LaserHand laserHand = hand.GetComponent<LaserHand>();

                if (laserHand != null)
                {
                    laserHand.active = false;
                    laserHand.thickness = 0;
                }
            }
        }
    }

    // Асинхронная загрузка сцены
    private static IEnumerator LoadSceneAsync(string sceneName)
    { 
        yield return new WaitForSeconds(3f);

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    private static SceneController instance;
    private static SceneController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneController>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("SceneController");
                    instance = obj.AddComponent<SceneController>();
                }
            }
            return instance;
        }
    }

    public void LoadSceneWithMode(string sceneName, GameKitchenModeData.GameMode mode)
    {
        GameKitchenModeData.CurrentGameMode = mode;

        SwitchScene(sceneName);
    }
}