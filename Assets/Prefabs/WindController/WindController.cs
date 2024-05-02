using UnityEngine;
using UnityEngine.XR.OpenXR.Input;
using Valve.VR;

public class WindController : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;
    public ParticleSystem windParticlesLeft;
    public ParticleSystem windParticlesRight;
    public SteamVR_Action_Boolean grabPinchLeftAction; // Действие кнопки Grab Pinch для левой руки
    public SteamVR_Action_Boolean grabPinchRightAction; // Действие кнопки Grab Pinch для правой руки
    public float windForce; // Сила ветра

    void Update()
    {
        if (leftController != null && rightController != null)
        {
            windParticlesLeft.transform.position = leftController.transform.position;
            windParticlesLeft.transform.rotation = leftController.transform.rotation;
            windParticlesRight.transform.position = rightController.transform.position;
            windParticlesRight.transform.rotation = rightController.transform.rotation;

            // Подписка на события нажатия/отпускания кнопок Grab Pinch для обеих рук
            grabPinchLeftAction.AddOnChangeListener(OnGrabPinchLeftActionChange, SteamVR_Input_Sources.LeftHand);
            grabPinchRightAction.AddOnChangeListener(OnGrabPinchRightActionChange, SteamVR_Input_Sources.RightHand);
            //ApplyWindToSphere(leftController, windParticlesLeft); // Применить воздействие ветра к шару с левого контроллера
            //ApplyWindToSphere(rightController, windParticlesRight); // Применить воздействие ветра к шару с правого контроллера

        }
    }
    private void OnDestroy()
    {
        // Отписка от событий при уничтожении объекта
        grabPinchLeftAction.RemoveOnChangeListener(OnGrabPinchLeftActionChange, SteamVR_Input_Sources.LeftHand);
        grabPinchRightAction.RemoveOnChangeListener(OnGrabPinchRightActionChange, SteamVR_Input_Sources.RightHand);
    }

    private void OnGrabPinchLeftActionChange(SteamVR_Action_Boolean action, SteamVR_Input_Sources inputSource, bool newValue)
    {
        // Включение/выключение системы частиц для левой руки
        if (newValue)
        {
            StartParticle(windParticlesLeft);
        }
        else
        {
            StopParticle(windParticlesLeft);
        }
    }

    private void OnGrabPinchRightActionChange(SteamVR_Action_Boolean action, SteamVR_Input_Sources inputSource, bool newValue)
    {
        // Включение/выключение системы частиц для правой руки
        if (newValue)
        {
            StartParticle(windParticlesRight);
        }
        else
        {
            StopParticle(windParticlesRight);
        }
    }

    private void StartParticle(ParticleSystem particleSystem)
    {
        // Включение системы частиц
        particleSystem.Play();
    }

    private void StopParticle(ParticleSystem particleSystem)
    {
        // Выключение системы частиц
        particleSystem.Stop();
    }


    void ApplyWindToSphere(GameObject controller, ParticleSystem windParticles)
    {
        if (windParticles.isPlaying)
        {
            Vector3 windDirection = controller.transform.forward; // Направление ветра соответствует направлению контроллера
            Vector3 windPosition = controller.transform.position;

           

            Collider[] colliders = Physics.OverlapSphere(windPosition, 2f); // Обнаружение объектов в радиусе ветра
            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 directionToCollider = collider.transform.position - windPosition;
                    float dotProduct = Vector3.Dot(windDirection, directionToCollider.normalized);
                    if (dotProduct > 0) // Проверка, что объект находится впереди контроллера
                    {
                        rb.AddForce(windDirection * windForce, ForceMode.Acceleration); // Применить силу ветра к объекту
                    }
                }
            }
        }
    }
}