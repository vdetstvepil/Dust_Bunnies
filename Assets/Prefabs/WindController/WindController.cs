using UnityEngine;
using Valve.VR;

public class WindController : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;
    public ParticleSystem windParticlesLeft;
    public ParticleSystem windParticlesRight;
    public SteamVR_Action_Single grabPinchLeftAction; // Действие силы нажатия Grab Pinch для левой руки
    public SteamVR_Action_Single grabPinchRightAction; // Действие силы нажатия Grab Pinch для правой руки
    public float maxWindForce; // Максимальная сила ветра
    public float maxSpeedModifier; // Максимальный множитель скорости

    private void Update()
    {
        if (leftController != null && rightController != null)
        {
            // Обновляем позиции и вращения систем частиц
            UpdateParticleSystemPosition(windParticlesLeft, leftController);
            UpdateParticleSystemPosition(windParticlesRight, rightController);

            // Обновляем силу ветра и множитель скорости в зависимости от силы нажатия
            UpdateWindForceAndSpeedModifier(windParticlesLeft, grabPinchLeftAction);
            UpdateWindForceAndSpeedModifier(windParticlesRight, grabPinchRightAction);
        }
    }

    private void UpdateParticleSystemPosition(ParticleSystem particleSystem, GameObject controller)
    {
        particleSystem.transform.position = controller.transform.position;
        particleSystem.transform.rotation = controller.transform.rotation;
    }

    private void UpdateWindForceAndSpeedModifier(ParticleSystem particleSystem, SteamVR_Action_Single grabPinchAction)
    {
        float pinchValue = grabPinchAction.GetAxis(SteamVR_Input_Sources.Any);

        // Обновляем множитель скорости частиц
        var velocityOverLifetime = particleSystem.velocityOverLifetime;
        velocityOverLifetime.speedModifier = pinchValue * maxSpeedModifier;

        // Обновляем силу ветра
        var collision = particleSystem.collision;
       // collision.colliderForce = pinchValue * maxWindForce;

        // Включаем/выключаем систему частиц в зависимости от силы нажатия
        if (pinchValue > 0)
        {
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }
        else
        {
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop();
            }
        }
    }
}
