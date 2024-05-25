using UnityEngine;
using Valve.VR;

public class BunnyFollower : MonoBehaviour
{
    public SteamVR_Action_Boolean grabGripAction; // Ссылка на действие нажатия кнопки Grab Grip
    private GameObject bunny;
    private Transform clump;
    private Transform player;
    private bool isFollowing = false;
    private Vector3 previousPosition;
    private Vector3 direction;
    public float followDistance = 2.0f; // Расстояние следования
    public float smoothSpeed = 2.0f; // Скорость сглаживания

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Предполагается, что игрок помечен тегом "Player"
    }

    void Update()
    {
        if (grabGripAction.GetStateDown(SteamVR_Input_Sources.Any))
        {
            StartFollowing();
        }

        if (grabGripAction.GetStateUp(SteamVR_Input_Sources.Any))
        {
            StopFollowing();
        }

        if (isFollowing && clump != null)
        {
            FollowTarget();
        }
    }

    void StartFollowing()
    {
        bunny = GameObject.Find("Bunny");
        if (bunny != null)
        {
            clump = bunny.transform.Find("low.clump");
            if (clump != null)
            {
                isFollowing = true;
                previousPosition = clump.position;
            }
        }
    }

    void StopFollowing()
    {
        isFollowing = false;
    }

    void FollowTarget()
    {
        // Определяем направление движения
        Vector3 newDirection = (clump.position - previousPosition).normalized;
        direction = Vector3.Lerp(direction, newDirection, Time.deltaTime * smoothSpeed);
        previousPosition = clump.position;

        // Определяем позицию для игрока позади low.clump на некотором расстоянии
        Vector3 followPosition = clump.position - direction * followDistance; // Настройте расстояние по вкусу
        followPosition.y = player.position.y; // Поддерживаем текущую высоту игрока

        // Плавно перемещаем игрока к followPosition
        player.position = Vector3.Lerp(player.position, followPosition, Time.deltaTime * smoothSpeed);

        // Плавно поворачиваем игрока в направлении движения, игнорируя ось X и Z
        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
        if (lookDirection != Vector3.zero) // Избегаем нулевого вектора
        {
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            player.rotation = Quaternion.Slerp(player.rotation, lookRotation, Time.deltaTime * smoothSpeed);
        }
    }
}
