using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnySplineController : MonoBehaviour
{
    private SplineFollower splineFollower;

    // Start is called before the first frame update
    void Start()
    {
        splineFollower = GetComponent<SplineFollower>(); 
        Vector3 objectPosition = transform.position;
    }

    public void ChekingBunnyPosition(Vector3 objectPosition)
    {
        float minX = -4.5f;
        float maxX = -5.5f;
        float minY = 0.0f;
        float maxY = -1.0f;
        float minZ = 11.0f; 
        float maxZ = 12.0f;
        if (objectPosition.x >= minX && objectPosition.x <= maxX &&
            objectPosition.y >= minY && objectPosition.y <= maxY &&
            objectPosition.z >= minZ && objectPosition.z <= maxZ)
        {
            ToughtButtonKachely();
        }
    }

    public void ToughtButtonKachely()
    {
        if (splineFollower != null)
        {
            splineFollower.enabled = true;
        }
    }
}