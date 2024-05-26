using UnityEngine;
using Valve.VR;

public class BunnyNavigation : MonoBehaviour
{
    public GameObject navigatorIcon; // Иконка навигатора
    public SteamVR_Behaviour_Pose controllerPose; // Позволяет отслеживать позицию и ориентацию контроллера

    private Camera playerCamera;
    private GameObject bunny; // Объект Bunny

    void Start()
    {
        playerCamera = Camera.main; // Основная камера
        navigatorIcon.SetActive(false); // Изначально скрыть иконку
    }

    void Update()
    {
        // Найти объект Bunny в сцене на каждом кадре
        bunny = GameObject.Find("low.clump");

        // Проверяем наличие объекта Bunny
        if (bunny != null)
        {
            Vector3 viewPos = playerCamera.WorldToViewportPoint(bunny.transform.position);
            bool isBunnyInView = viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0;

            if (!isBunnyInView)
            {
                navigatorIcon.SetActive(true);
                UpdateNavigatorIcon();
            }
            else
            {
                navigatorIcon.SetActive(false);
            }
        }
        else
        {
            navigatorIcon.SetActive(false); // Если Bunny отсутствует, скрыть курсор
        }
    }

    void UpdateNavigatorIcon()
    {
        if (bunny != null)
        {
            Vector3 bunnyDirection = (bunny.transform.position - controllerPose.transform.position).normalized;
            bunnyDirection.y = 0; // Убираем вертикальный компонент направления, чтобы иконка была на уровне горизонта

            // Позиция иконки над рукой
            Vector3 iconPosition = controllerPose.transform.position + Vector3.up * 0.07f;
            navigatorIcon.transform.position = iconPosition;

            // Поворот иконки, чтобы она указывала на Bunny
            Quaternion iconRotation = Quaternion.LookRotation(bunnyDirection);
            navigatorIcon.transform.rotation = iconRotation;
        }
    }
}
