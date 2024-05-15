using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SceneStart : MonoBehaviour
{
    public Vector3 position; // Координаты для перемещения
    public Vector3 rotation; // Углы поворота

    void Start()
    { 
        SetCameraPosition();

        // Убираем черный экран
        SteamVR_Fade.Start(Color.black, 0);
        SteamVR_Fade.Start(Color.clear, 3f);
    }

    private void SetCameraPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Перемещаем камеру в указанные координаты
            player.transform.position = position;
            player.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
