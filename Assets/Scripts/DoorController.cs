using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class DoorController : MonoBehaviour
{
    public string chosenDoor;

    private Animator animator;


    private void Start()
    {
        // Получаем компонент Animator из текущего объекта
        animator = GetComponent<Animator>();
    }

    // Метод для запуска анимации
    public void OpenDoor()
    {
        if (animator != null)
        {
            animator.enabled = true;
            animator.Play("DoorOpen");
        }
    }

    public void LoadScene()
    {
        switch (chosenDoor)
        {
            case "Kitchen":
                GameKitchenModeData.CurrentGameMode = GameKitchenModeData.GameMode.Start;
                SceneController.SwitchScene("Kitchen");
                break;
            case "LivingRoom":
                SceneController.SwitchScene("LivingRoom");
                break;
            case "KidRoom":
                SceneController.SwitchScene("KidRoom");
                break;
            case "Start":
                SceneController.SwitchScene("Start");
                break;
        }
    }
}