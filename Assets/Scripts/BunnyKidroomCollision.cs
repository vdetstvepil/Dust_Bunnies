using UnityEngine;
using UnityEngine.SceneManagement;

public class BunnyKidroomCollision : MonoBehaviour
{
    private HeadCountController timerController;

    void Start()
    {
        timerController = FindObjectOfType<HeadCountController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (SceneManager.GetActiveScene().name == "KidRoom")
        {
            if (collision.gameObject.CompareTag("Score"))
            {
                timerController.AddPoints(1);
                Destroy(collision.gameObject);
            }
        }
    }
}
