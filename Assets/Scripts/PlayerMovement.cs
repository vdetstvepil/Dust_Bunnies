using UnityEngine;
using Valve.VR;

public class PlayerMovement : MonoBehaviour
{
    public SteamVR_Input_Sources inputSource; // Источник ввода (левый или правый контроллер)
    public SteamVR_Action_Vector2 movementAction; // Действие для получения входных данных от джойстика
    public float maxSpeed = 1.0f; // Максимальная скорость перемещения
    public float acceleration = 1.0f; // Ускорение
    public float deceleration = 2.0f; // Замедление
    private Transform playerCamera; // Ссылка на камеру игрока
    private Vector3 currentVelocity; // Текущая скорость перемещения

    private void Start()
    {
        playerCamera = GetComponentInChildren<Camera>().transform; // Получаем ссылку на камеру игрока
    }

    private void Update()
    {
        // Получаем вводные данные от джойстика
        Vector2 movementInput = movementAction.GetAxis(inputSource);

        // Получаем направление взгляда игрока в плоскости XZ (горизонтальной плоскости)
        Vector3 cameraForward = Vector3.Scale(playerCamera.forward, new Vector3(1, 0, 1)).normalized;

        // Преобразуем вводные данные в вектор движения относительно направления взгляда
        Vector3 movement = cameraForward * movementInput.y + playerCamera.right * movementInput.x;

        // Применяем максимальную скорость и время к вектору движения
        movement = Vector3.ClampMagnitude(movement, 1) * maxSpeed * Time.deltaTime;

        // Применяем ускорение или замедление к текущей скорости в соответствии с вводом
        if (movementInput.magnitude > 0)
        {
            currentVelocity = Vector3.Lerp(currentVelocity, movement, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        // Применяем скорость перемещения к игроку
        transform.position += currentVelocity;
    }
}
