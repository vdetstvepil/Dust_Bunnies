using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SceneStart : MonoBehaviour
{
    void Start()
    {
        // Убираем черный экран
        SteamVR_Fade.Start(Color.black, 0);
        SteamVR_Fade.Start(Color.clear, 3f);
    }
}
