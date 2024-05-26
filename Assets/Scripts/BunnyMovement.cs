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
    private Animator animator;
    private Rigidbody clumpRigidbody;
    private bool isMoving = false;
    private float speedThreshold = 0.25f;
    private float minAnimationSpeed = 0.1f;
    private float rotationSpeed = 5.0f;

    private void Start()
    {
        objects = new GameObject[] { ears, eyeballs, eyes, nose };
        animator = GetComponent<Animator>();
        clumpRigidbody = clump.GetComponent<Rigidbody>();
    }

    void Update()
    {
        foreach (GameObject gameObject in objects)
        {
            // Устанавливаем позицию объекта равной позиции шарика
            gameObject.transform.position = clump.transform.position;

            // Получаем направление, куда смотрит шарик (его скорость)
            Vector3 ballDirection = clumpRigidbody.velocity.normalized;

            // Находим угол между вектором направления шарика и осью Z
            float angle = Mathf.Atan2(ballDirection.x, ballDirection.z) * Mathf.Rad2Deg;

            // Рассчитываем целевую ротацию
            Quaternion targetRotation = Quaternion.Euler(-90, angle + 180f, 0);

            // Плавно поворачиваем объект к целевой ротации
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Проверяем скорость шарика и управляем анимациями
        float speed = clumpRigidbody.velocity.magnitude;

        // Устанавливаем скорость анимации, учитывая минимальную скорость
        float animationSpeed = Mathf.Max(speed, minAnimationSpeed);
        animator.SetFloat("Speed", animationSpeed);

        if (speed > speedThreshold && !isMoving)
        {
            animator.ResetTrigger("StopGo");
            animator.SetTrigger("StartGo");
            isMoving = true;
        }
        else if (speed <= speedThreshold && isMoving)
        {
            animator.ResetTrigger("StartGo");
            animator.SetTrigger("StopGo");
            isMoving = false;
        }
    }
}
