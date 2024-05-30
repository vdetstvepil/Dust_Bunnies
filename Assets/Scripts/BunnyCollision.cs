using UnityEngine;
using UnityEngine.SceneManagement;

public class BunnyCollision : MonoBehaviour
{
    private HeadTimerController timerController; // Ссылка на контроллер таймера

    void Start()
    {
        // Находим контроллер таймера в сцене
        timerController = FindObjectOfType<HeadTimerController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (SceneManager.GetActiveScene().name == "Kitchen")
        {
            // Проверяем столкновение с объектом, имеющим тег "Enemy"
            if (collision.gameObject.CompareTag("Enemy"))
            {
                // Уменьшаем жизнь в HandTimerController
                timerController.LoseLife();
            }
            if (collision.gameObject.CompareTag("Score"))
            {
                timerController.AddScore(100);
                Destroy(collision.gameObject);
            }
        }
    }
}
