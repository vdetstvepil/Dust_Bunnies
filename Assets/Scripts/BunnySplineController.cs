using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnySplineController : MonoBehaviour
{
    private SplineFollower splineFollower;
    [SerializeField] private GameObject bunny;


    // Start is called before the first frame update
    void Start()
    {
        splineFollower = GetComponent<SplineFollower>(); 
        Vector3 objectPosition = transform.position;
    }

    public void ChekingBunnyPosition(Vector3 objectPosition)
    {
        float minX = 5.0f;
        float maxX = 10.0f;
        float minY = 2.0f;
        float maxY = 8.0f;
        float minZ = -3.0f;
        float maxZ = 3.0f;
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