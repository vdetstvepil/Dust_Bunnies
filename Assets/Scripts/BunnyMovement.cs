using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMovement : MonoBehaviour
{
    public GameObject ears;
    public GameObject eyeballs;
    public GameObject eyes;
    public GameObject nose;
    public GameObject clump;

    private GameObject[] objects;

    private void Start()
    {
        objects = new GameObject[] { ears, eyeballs, eyes, nose };
    }

    void Update()
    {
        foreach (GameObject gameObject in objects)
        {
            // Устанавливаем позицию объекта равной позиции шарика
            gameObject.transform.position = clump.transform.position;

            // Получаем направление, куда смотрит шарик (его скорость)
            Vector3 ballDirection = clump.GetComponent<Rigidbody>().velocity.normalized;

            // Находим угол между вектором направления шарика и осью Z
            float angle = Mathf.Atan2(ballDirection.x, ballDirection.z) * Mathf.Rad2Deg;

            // Поворачиваем объект на этот угол, оставляя вращение по оси Y
            gameObject.transform.rotation = Quaternion.Euler(-90, angle + 180f, 0);
        }
       
    }
}

