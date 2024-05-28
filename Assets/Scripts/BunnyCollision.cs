using UnityEngine;

public class BunnyCollision : MonoBehaviour
{
    private HandTimerController timerController; // Ссылка на контроллер таймера

    void Start()
    {
        // Находим контроллер таймера в сцене
        timerController = FindObjectOfType<HandTimerController>();
        if (timerController == null)
        {
            Debug.LogError("HandTimerController не найден в сцене!");
        }
        Debug.Log("Hand found");
    }

    void OnCollisionEnter(Collision collision)
    {
        // Проверяем столкновение с объектом, имеющим тег "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Уменьшаем жизнь в HandTimerController
            timerController.LoseLife();
            Debug.Log("-life");
        }
    }
}
